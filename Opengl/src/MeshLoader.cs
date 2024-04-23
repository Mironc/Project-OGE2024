using System.Collections.Generic;
using Graphic;
using OpenTK;
using Tools;
    public static class MeshLoader
    {
        public static Mesh<ModelVertex> LoadMesh(string source)
        {
            var Positions = new List<Vector3>();
            var Normals = new List<Vector3>();
            var TextureCoordinates = new List<Vector2>();
            var Indices = new List<uint>();
            var Vertexes = new List<ModelVertex>();
            var SourceByLines = source.Split('\n');
            uint n = 0;
            foreach(string line in SourceByLines)
            {
                var LineBySpaces = line.Split(' ');
                switch (LineBySpaces[0]) 
                {
                    case "v": Positions.Add(new Vector3(
                        float.Parse(LineBySpaces[1]),
                        float.Parse(LineBySpaces[2]),
                        float.Parse(LineBySpaces[3])));
                        break;
                    case "vt": TextureCoordinates.Add(new Vector2(
                            float.Parse(LineBySpaces[1]),
                            float.Parse(LineBySpaces[2])));
                        break;
                    case "vn": Normals.Add(new Vector3(
                            float.Parse(LineBySpaces[1]), 
                            float.Parse(LineBySpaces[2]), 
                            float.Parse(LineBySpaces[3])));
                        break;
                    case "f":
                        for (int i = 1; i < LineBySpaces.Length ; i++ ){
                        var indices = LineBySpaces[i].Split('/');
                            Vertexes.Add(new ModelVertex(Positions[int.Parse(indices[0]) - 1],
                                 Normals[ int.Parse(indices[2]) - 1],
                                    TextureCoordinates[ int.Parse(indices[1]) - 1]));
                            Indices.Add(n);
                            ++n;
                        }
                        break;
                }
            }
        return new Mesh<ModelVertex>(Vertexes.ToArray(),Indices.ToArray());
        }
    }
    public struct ModelVertex : IVertex
    {
        Vector3 Position;
        Vector3 Normal;
        Vector2 TextureCoordinate;
        public ModelVertex(Vector3 Position,Vector3 Normal,Vector2 TextureCoordinate)
        {
            this.Position = Position;
            this.Normal = Normal;
            this.TextureCoordinate = TextureCoordinate;
        }
        public void Description()
        {
            var VertexBuilder = new VertexBuilder<ModelVertex>();
            VertexBuilder.Float(3, false);
            VertexBuilder.Float(3, false);
            VertexBuilder.Float(2, false);
        }
    }
