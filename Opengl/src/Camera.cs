using System;
using OpenTK;
using Graphic;
public sealed class Camera
{
    private readonly float ZFar, ZNear;
    private readonly Func<float> AspectRatioGetter;
    public float Fov {get;set;}
    public Matrix4 View { get; private set; }
    public Matrix4 Projection { get; private set; }
    public Camera(float Fov,float ZNear,float ZFar,Viewport viewport)
    {
        this.Fov = Fov;
        this.ZNear = ZNear;
        this.ZFar = ZFar;
        this.AspectRatioGetter = () => viewport.AspectRatio;
    }
    public void CalculateView(Transform transform)
    {
        transform.GetMatrix();
        this.View = Matrix4.LookAt(transform.Position,transform.Position+transform.Forward, transform.Up);
    }
    public void CalculateProjection()
    {
        this.Projection =
        Matrix4.CreatePerspectiveFieldOfView(MathHelper.DegreesToRadians(this.Fov), this.AspectRatioGetter(), this.ZNear, this.ZFar);
    }
}
