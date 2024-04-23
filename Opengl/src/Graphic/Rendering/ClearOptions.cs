using System;
namespace Graphic.Rendering
{
    public enum ClearFlag
    {
        Depth = 0x100,
        Color = 0x4000,
        Stencil = 0x400,
    }
    public class ClearOptions
    {
        public int Flags{get;private set;} = 0;
        public void AddFlag(ClearFlag flag)
        {
            this.Flags |= (int)flag;
        }
        public void RemoveFlag(ClearFlag flag)
        {
            this.Flags &= ~(int)flag;
        }
        public void ClearFlags()
        {
            Flags = 0;
        }
    }
}
