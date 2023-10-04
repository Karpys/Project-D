using UnityEngine;
using UnityEngine.UI;

namespace TweenCustom
{
    public static class TweenExtensions
    {
        // Start is called before the first frame update

        #region DoType



        public static BaseTween DoMove(this Transform trans, Vector3 endValue,float duration)
        {
            TweenMove baseTween = new TweenMove(trans, endValue,duration);
            TweenManager.Instance.AddTween(baseTween);
            return baseTween;
        }
        
        public static BaseTween DoMoveX(this Transform trans, float endValue,float duration)
        {
            TweenMoveX baseTween = new TweenMoveX(trans, endValue,duration);
            TweenManager.Instance.AddTween(baseTween);
            return baseTween;
        }
        
        public static BaseTween DoMoveY(this Transform trans, float endValue,float duration)
        {
            TweenMoveY baseTween = new TweenMoveY(trans, endValue,duration);
            TweenManager.Instance.AddTween(baseTween);
            return baseTween;
        }

        public static BaseTween DoLocalMove(this Transform trans, Vector3 endValue,float duration)
        {
            TweenLocalMove baseTween = new TweenLocalMove(trans, endValue, duration);
            TweenManager.Instance.AddTween(baseTween);
            return baseTween;
        }
        
        public static BaseTween DoRotate(this Transform trans, Vector3 endValue,float duration)
        {
            TweenRotate baseTween = new TweenRotate(trans, endValue, duration);
            TweenManager.Instance.AddTween(baseTween);
            return baseTween;
        }
        
        public static BaseTween DoPivotRotate(this Transform trans, float endValue,float duration)
        {
            TweenPivotRotate baseTween = new TweenPivotRotate(trans, endValue, duration);
            TweenManager.Instance.AddTween(baseTween);
            return baseTween;
        }

        public static BaseTween DoScale(this Transform trans, Vector3 endValue, float duration)
        {
            TweenScale baseTween = new TweenScale(trans, endValue, duration);
            TweenManager.Instance.AddTween(baseTween);
            return baseTween;
        }
        
        public static BaseTween DoUIPosition(this Transform trans, Vector3 endValue, float duration)
        {
            TweenUIPosition baseTween = new TweenUIPosition(trans, endValue, duration);
            TweenManager.Instance.AddTween(baseTween);
            return baseTween;
        }
        
        public static BaseTween DoColor(this SpriteRenderer renderer, Color endValue,float duration)
        {
            TweenSpriteColor baseTween = new TweenSpriteColor(renderer, endValue.rgba(), duration);
            TweenManager.Instance.AddTween(baseTween);
            return baseTween;
        }
        
        public static BaseTween DoColor(this Image renderer, Color endValue,float duration)
        {
            TweenImageColor baseTween = new TweenImageColor(renderer, endValue.rgba(), duration);
            TweenManager.Instance.AddTween(baseTween);
            return baseTween;
        }
        
        public static void DoKill(this Transform trans)
        {
            TweenManager.Instance.KillTween(trans);
        }

        /*public static BaseTween DoMove(this Transform trans, float duration, Vector3 endValue,Ease ease)
        {
            TweenMove baseTween = new TweenMove(trans, duration, endValue,ease);
            TweenManager.Instance.AddTween(baseTween);
            return baseTween;
        }*/

        #endregion

        #region DoType Surcharge
        public static BaseTween DoMove(this Transform trans, BaseTweenData baseData)
        {
            TweenMove baseTween = new TweenMove(baseData.TargetTransform, baseData.EndValue,baseData.Duration);
            TweenManager.Instance.AddTween(baseTween);
            return baseTween;
        }
        
        public static BaseTween DoLocalMove(this Transform trans, BaseTweenData baseData)
        {
            TweenLocalMove baseTween = new TweenLocalMove(baseData.TargetTransform, baseData.EndValue, baseData.Duration);
            TweenManager.Instance.AddTween(baseTween);
            return baseTween;
        }
        
        public static BaseTween DoRotate(this Transform trans, BaseTweenData baseData)
        {
            TweenRotate baseTween = new TweenRotate(baseData.TargetTransform, baseData.EndValue, baseData.Duration);
            TweenManager.Instance.AddTween(baseTween);
            return baseTween;
        }

        public static BaseTween DoScale(this Transform trans, BaseTweenData baseData)
        {
            TweenScale baseTween = new TweenScale(baseData.TargetTransform, baseData.EndValue, baseData.Duration);
            TweenManager.Instance.AddTween(baseTween);
            return baseTween;
        }


        #endregion
        #region DoParameters

        public static BaseTween SetEase(this BaseTween tween,Ease ease)
        {
            tween.ease = ease;
            return tween;
        }
        
        public static BaseTween SetCurve(this BaseTween tween,AnimationCurve curve)
        {
            tween.curve = curve;
            return tween;
        }

        public static BaseTween SetDelay(this BaseTween tween, float delay)
        {
            tween.delay = delay;
            return tween;
        }

        public static BaseTween SetMode(this BaseTween tween, TweenMode mode)
        {
            tween.tweenMode(mode);
            return tween;
        }

        #endregion

        #region TweenCall

        public static BaseTween OnComplete(this BaseTween tween,TweenAction action)
        {
            tween.m_onComplete = action;
            return tween;
        }
        public static BaseTween OnStart(this BaseTween tween, TweenAction action)
        {
            tween.m_onStart = action;
            return tween;
        }
        
        public static BaseTween OnReferenceLose(this BaseTween tween, TweenAction action)
        {
            tween.m_onReferenceLose = action;
            return tween;
        }

        #endregion
    }

    public class TweenPivotRotate : BaseTween
    {
        public TweenPivotRotate(Transform target,float endValue,float duration)
        {
            m_Target = target;
            m_Duration = duration;
            m_EndValue.y = endValue;
            m_StartValue.y = target.eulerAngles.y;
            
            if (Mathf.Abs(m_StartValue.y - m_EndValue.y) > 180)
            {
                if (m_StartValue.y > 180 && m_EndValue.y < 180)
                {
                    m_StartValue.y -= 360f;
                }
            }
        }

        protected override void Update()
        {
            Vector3 eulerAngles = m_Target.eulerAngles;
            eulerAngles = new Vector3(eulerAngles.x, NewAngle(), eulerAngles.z);
            m_Target.eulerAngles = eulerAngles;
        }

        private float NewAngle()
        {
            return Mathf.LerpUnclamped(m_StartValue.y, m_EndValue.y, (float)Evaluate());
        }

        public override void TweenRefreshStartValue()
        {
            m_StartValue = m_Target.eulerAngles;
        }
    }
}
