using OpenTK;
public sealed class Transform
{
    private bool _dirty = true;
    public Vector3 Position { get; private set; }
    public Vector3 Forward { get; private set; }
    public Vector3 Up { get; private set; }
    public Vector3 Right { get; private set; }
    public Vector3 Rotation { get; private set; }
    private Matrix4 _matrix;
    public Transform(Vector3? Position = null, Vector3? Rotation = null)
    {
        this.Position = Position ?? Vector3.Zero;
        this.Rotation = Rotation ?? Vector3.Zero;
        this.Forward = Vector3.UnitZ;
        this.Up = Vector3.UnitY;
        this.Right = Vector3.UnitX;
        GetMatrix();
    }
    public void Translate(Vector3 translation)
    {
        this.Position += translation;
        _dirty = true;
    }
    public void Rotate(Vector3 Rotation)
    {
        this.Rotation += Rotation;
        _dirty = true;
    }
    public Matrix4 GetMatrix()
    {
        if (_dirty) {
            this._matrix = this.RotationMatrix() * Matrix4.CreateTranslation(this.Position);
            _dirty = false;
        }
        return _matrix;
    }
    private Matrix4 RotationMatrix()
    {
        var _RotationMatrix = Matrix4.CreateRotationX(MathHelper.DegreesToRadians(this.Rotation.X))
                * Matrix4.CreateRotationY(MathHelper.DegreesToRadians(this.Rotation.Y))
                * Matrix4.CreateRotationZ(MathHelper.DegreesToRadians(this.Rotation.Z));
        System.Console.WriteLine(this.Rotation);
        this.Forward = Vector3.UnitZ * new Matrix3(_RotationMatrix);
        this.Up = Vector3.UnitY * new Matrix3(_RotationMatrix);
        this.Right = Vector3.UnitX * new Matrix3(_RotationMatrix);
        return _RotationMatrix;
    }
}