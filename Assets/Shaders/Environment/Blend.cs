namespace UnityEngine.Rendering
{
  using System.Collections;
  using System.Collections.Generic;
  using UnityEngine;

  public class Blend
  {
    public static Vector2Int GetBlendVector(BlendAssociation blend)
    {
      switch (blend)
      {
        default:
        case BlendAssociation.Opaque:
          return new Vector2Int(1, 0);
        case BlendAssociation.Transparent:
          return new Vector2Int(5, 10);
        case BlendAssociation.TransparentPremultiplied:
          return new Vector2Int(1, 10);
        case BlendAssociation.Additive:
          return new Vector2Int(1, 1);
        case BlendAssociation.AdditiveSoft:
          return new Vector2Int(4, 1);
        case BlendAssociation.Multiply:
          return new Vector2Int(2, 0);
        case BlendAssociation.MultiplyDouble:
          return new Vector2Int(2, 3);
      }
    }
    public static BlendAssociation GetBlend(Vector2Int blend)
    {
      if (blend.x == 1 && blend.y == 0)
        return BlendAssociation.Opaque;
      if (blend.x == 5 && blend.y == 10)
        return BlendAssociation.Transparent;
      if (blend.x == 1 && blend.y == 10)
        return BlendAssociation.TransparentPremultiplied;
      if (blend.x == 1 && blend.y == 1)
        return BlendAssociation.Additive;
      if (blend.x == 4 && blend.y == 1)
        return BlendAssociation.AdditiveSoft;
      if (blend.x == 2 && blend.y == 0)
        return BlendAssociation.Multiply;
      if (blend.x == 2 && blend.y == 3)
        return BlendAssociation.MultiplyDouble;

      return BlendAssociation.Opaque;
    }

    public enum BlendAssociation
    {
      Opaque = 0,
      Transparent = 1,
      TransparentPremultiplied = 2,
      Additive = 3,
      AdditiveSoft = 4,
      Multiply = 5,
      MultiplyDouble = 6,
    }
  }
}