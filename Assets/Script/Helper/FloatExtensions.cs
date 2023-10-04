namespace KarpysDev.Script.Helper
{
    public static class FloatExtensions
    {
        public static float ToAbsoluteAngle(this float angle)
        {
            if (angle < 0)
            {
                angle += 360;
                return angle;
            }

            return angle;
        }
    }
}