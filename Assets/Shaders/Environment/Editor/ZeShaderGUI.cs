using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;

public class ZeShaderGUI : ShaderGUI
{
  private Material _targetMat;
  private MaterialEditor _editor;
  private MaterialProperty[] _properties;

  private FontStyle _origFontStyle;

  [SerializeField] private LightingModel _lighting;
  [SerializeField] private bool _clipping;
  [SerializeField] private Blend.BlendAssociation _blending;
  [SerializeField] private CullingMode _culling;
  [SerializeField] private bool _useNormal;
  [SerializeField] private bool _useDetail;
  [SerializeField] private DetailBlend _detBlend;
  [SerializeField] private bool _useVCol;
  [SerializeField] private bool _fog;
  [SerializeField] private int _zTest = 2;
  [SerializeField] private bool _showLightmap;

  public override void OnGUI(MaterialEditor materialEditor, MaterialProperty[] properties)
  {
    _editor = materialEditor;
    _properties = properties;
    _targetMat = materialEditor.target as Material;

    EditorGUI.BeginChangeCheck();

    EditorStyles.toggle.fontStyle = _origFontStyle = FontStyle.Normal;

    HandleLightingModel();

    HandleBlending();

    HandleCulling();

    HandleMainParams();

    HandleDetailParams();

    if (_lighting != LightingModel.Unlit)
    {
      HandleLightingParams();
      HandleNormalMap();
    }

    HandleVertexColors();

    GUILayout.Space(10);

    HandleFog();
    HandleZTest();
    HandleShowLightmap();
    _editor.EnableInstancingField();
    _editor.RenderQueueField();

    if (EditorGUI.EndChangeCheck())
    {
      RecordAction("Material Change");
      ApplyShaderKeywords();
    }
  }

  private void HandleBlending()
  {
    Vector2Int blendParams = Vector2Int.zero;

    blendParams.x = (int)_targetMat.GetFloat("_SrcBlend");
    blendParams.y = (int)_targetMat.GetFloat("_DstBlend");
    _blending = Blend.GetBlend(blendParams);

    _blending = (Blend.BlendAssociation)EditorGUILayout.EnumPopup("Blending", _blending);
    blendParams = Blend.GetBlendVector(_blending);

    _targetMat.SetFloat("_SrcBlend", blendParams.x);
    _targetMat.SetFloat("_DstBlend", blendParams.y);

    _targetMat.SetFloat("_ZWrite", _blending == 0 ? 1 : 0);
    //_targetMat.renderQueue = _blending == 0 ? 2000 : 3000;
  }

  private void HandleCulling()
  {
    _culling = (CullingMode)EditorGUILayout.EnumPopup("Culling Mode", _culling);
    float cull;
    switch (_culling)
    {
      case CullingMode.Back:
        cull = 2;
        break;
      case CullingMode.Front:
        cull = 1;
        break;
      default:
      case CullingMode.Off:
        cull = 0;
        break;
    }
    _targetMat.SetFloat("_CullMode", cull);
  }

  private void HandleMainParams()
  {
    GUILayout.Space(10);
    GUILayout.Label("Main Maps", EditorStyles.boldLabel);
    GUILayout.Space(2);

    MaterialProperty mainTex = FindProperty("_MainTex");
    MaterialProperty color = FindProperty("_Color");
    _editor.TexturePropertySingleLine(new GUIContent(mainTex.displayName), mainTex, color);
    EditorGUI.indentLevel += 2;
    _editor.TextureScaleOffsetProperty(mainTex);
    EditorGUI.indentLevel -= 2;

    if (BoolHeader(ref _clipping, "_CLIPPING", "Clipping"))
    {
      MaterialProperty cutoff = FindProperty("_Cutoff");
      _editor.RangeProperty(cutoff, cutoff.displayName);
    }
  }

  private void HandleDetailParams()
  {
    BoolHeader(ref _useDetail, "_DETAIL", "Detail Map");

    if (_useDetail)
    {
      MaterialProperty detTex = FindProperty("_DetailTex");
      MaterialProperty color = FindProperty("_DetailColor");
      _editor.TexturePropertySingleLine(new GUIContent(detTex.displayName), detTex, color);
      
      EditorGUI.indentLevel += 2;
      _editor.TextureScaleOffsetProperty(detTex);

      bool ovrl = Array.IndexOf(_targetMat.shaderKeywords, "DET_ADD") != -1;
      bool mlt = Array.IndexOf(_targetMat.shaderKeywords, "_DET_MLT") != -1;

      if (ovrl)
        _detBlend = DetailBlend.Add;
      else if (mlt)
        _detBlend = DetailBlend.Multiply;
      else
        _detBlend = DetailBlend.Value;

      _detBlend = (DetailBlend)EditorGUILayout.EnumPopup("Blending", _detBlend);
    }
  }

  private void HandleLightingModel()
  {
    bool vLit = Array.IndexOf(_targetMat.shaderKeywords, "_VLIT") != -1;
    bool fLit = Array.IndexOf(_targetMat.shaderKeywords, "_FLIT") != -1;
    bool lightmapped = Array.IndexOf(_targetMat.shaderKeywords, "_LIGHTMAPPED") != -1;

    if (vLit)
      _lighting = LightingModel.VertexLit;
    else if (fLit)
      _lighting = LightingModel.FragmentLit;
    else if (lightmapped)
      _lighting = LightingModel.Lightmapped;
    else
      _lighting = LightingModel.Unlit;

    _lighting = (LightingModel)EditorGUILayout.EnumPopup("Lighting type", _lighting);
  }

  private void HandleLightingParams()
  {
    GUILayout.Space(10);
    GUILayout.Label("Shadows Mapping", EditorStyles.boldLabel);
    GUILayout.Space(2);
    MaterialProperty shadowRamp = FindProperty("_ShadowRamp");
    _editor.TexturePropertySingleLine(new GUIContent(shadowRamp.displayName), shadowRamp);
    EditorGUI.indentLevel += 2;
    _editor.TextureScaleOffsetProperty(shadowRamp);
    EditorGUI.indentLevel -= 2;

    MaterialProperty cutShadowColor = FindProperty("_CutShadowColor");
    MaterialProperty diffuseShadowColor = FindProperty("_DiffuseShadowColor");

    _editor.ColorProperty(cutShadowColor, cutShadowColor.displayName);
    _editor.ColorProperty(diffuseShadowColor, diffuseShadowColor.displayName);

    if (_lighting != LightingModel.Lightmapped)
      return;

    GUILayout.Space(10);
    GUILayout.Label("Lightmap Settings", EditorStyles.boldLabel);
    GUILayout.Space(2);

    MaterialProperty lightmapThreshold = FindProperty("_LightmapThreshold");
    _editor.FloatProperty(lightmapThreshold, lightmapThreshold.displayName);
  }

  private void HandleNormalMap()
  {
    if (_lighting == LightingModel.FragmentLit)
    {
      BoolHeader(ref _useNormal, "_NORMAL", "Normal Map");

      if (_useNormal)
      {
        MaterialProperty map = FindProperty("_NormalMap");
        _editor.TexturePropertySingleLine(new GUIContent(map.displayName), map);
        MaterialProperty scale = FindProperty("_NormalScale");
        _editor.FloatProperty(scale, scale.displayName);
      }
    }
    else
    {
      _useNormal = false;
    }
  }

  private void HandleVertexColors()
  {
    if (BoolHeader(ref _useVCol, "_VCOL", "Vertex Colors"))
    {
      MaterialProperty vcolInfluence = FindProperty("_VColInfluence");
      _editor.RangeProperty(vcolInfluence, vcolInfluence.displayName);
    }
  }

  private void HandleFog()
  {
    _fog = Array.IndexOf(_targetMat.shaderKeywords, "_FOG") != -1;
    _fog = EditorGUI.Toggle(
      EditorGUILayout.GetControlRect(true, 20f, EditorStyles.layerMaskField),
      EditorGUIUtility.TrTextContent("Enable Fog"),
      _fog);
  }

  private void HandleZTest()
  {
    _zTest = (int)_targetMat.GetFloat("_ZTest");
    _zTest = EditorGUI.Toggle(
      EditorGUILayout.GetControlRect(true, 20f, EditorStyles.layerMaskField),
      EditorGUIUtility.TrTextContent("Enable ZTest"),
      _zTest == 2 ? true : false) == true ? 2 : 6;
    _targetMat.SetFloat("_ZTest", _zTest);
  }

  private void HandleShowLightmap()
  {
    if (_lighting == LightingModel.Lightmapped)
    {
      _showLightmap = Array.IndexOf(_targetMat.shaderKeywords, "_SHOW_LIGHTMAP") != -1;
      _showLightmap = EditorGUI.Toggle(
        EditorGUILayout.GetControlRect(true, 20f, EditorStyles.layerMaskField),
        EditorGUIUtility.TrTextContent("Show Lightmap (debug)"),
        _showLightmap);
    }
    else
    {
      _showLightmap = false;
    }
  }

  private void ApplyShaderKeywords()
  {
    switch (_lighting)
    {
      default:
      case LightingModel.Unlit:
        SetKeyword("_VLIT", false);
        SetKeyword("_FLIT", false);
        SetKeyword("_LIGHTMAPPED", false);
        break;
      case LightingModel.VertexLit:
        SetKeyword("_VLIT", true);
        SetKeyword("_FLIT", false);
        SetKeyword("_LIGHTMAPPED", false);
        break;
      case LightingModel.FragmentLit:
        SetKeyword("_VLIT", false);
        SetKeyword("_FLIT", true);
        SetKeyword("_LIGHTMAPPED", false);
        break;
      case LightingModel.Lightmapped:
        SetKeyword("_VLIT", false);
        SetKeyword("_FLIT", false);
        SetKeyword("_LIGHTMAPPED", true);
        break;
    }

    SetKeyword("_CLIPPING", _clipping);

    SetKeyword("_NORMAL", _useNormal);

    SetKeyword("_DETAIL", _useDetail);

    switch (_detBlend)
    {
      default:
      case DetailBlend.Value:
        SetKeyword("DET_ADD", false);
        SetKeyword("_DET_MLT", false);
        break;
      case DetailBlend.Add:
        SetKeyword("DET_ADD", true);
        SetKeyword("_DET_MLT", false);
        break;
      case DetailBlend.Multiply:
        SetKeyword("DET_ADD", false);
        SetKeyword("_DET_MLT", true);
        break;
    }

    SetKeyword("_VCOL", _useVCol);

    SetKeyword("_FOG", _fog);

    SetKeyword("_SHOW_LIGHTMAP", _showLightmap);
  }

  private MaterialProperty FindProperty(string name)
  {
    return FindProperty(name, _properties);
  }

  void SetKeyword(string keyword, bool state)
  {
    if (state)
    {
      _targetMat.EnableKeyword(keyword);
    }
    else
    {
      _targetMat.DisableKeyword(keyword);
    }
  }

  void RecordAction(string label)
  {
    _editor.RegisterPropertyChangeUndo(label);
  }

  bool BoolHeader(ref bool privateBool, string shaderKeyword, string displayName)
  {
    GUILayout.Space(10);
    privateBool = Array.IndexOf(_targetMat.shaderKeywords, shaderKeyword) != -1;
    EditorStyles.toggle.fontStyle = FontStyle.Bold;
    privateBool = GUILayout.Toggle(privateBool, new GUIContent(displayName));
    EditorStyles.toggle.fontStyle = _origFontStyle;
    GUILayout.Space(2);
    return privateBool;
  }
}

public enum LightingModel
{
  Unlit = 0,
  VertexLit = 1,
  FragmentLit = 2,
  Lightmapped = 3,
}

public enum UVMap
{
  UV1 = 0,
  UV2 = 1,
  UV3 = 2,
}

public enum DetailBlend
{
  Value = 0,
  Add = 1,
  Multiply = 2,
}

public enum CullingMode
{
  Back = 0,
  Front = 1,
  Off = 2,
}