﻿using System;
using System.Collections.Generic;
using System.Linq;
using Assimp;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Pokemon3D.Rendering
{
    public struct ModelMesh
    {
        public Mesh Mesh;
        public Material Material;
    }

    public class MeshManager
    {
        private readonly GraphicsDevice _device;
        private readonly Dictionary<string, ModelMesh> _meshCache = new Dictionary<string, ModelMesh>();
        private readonly Mesh _cubeMesh;
        private readonly Mesh _billboardMesh;

        public MeshManager(GraphicsDevice device)
        {
            _device = device;
            _cubeMesh = new Mesh(_device, Primitives.GenerateCubeData());
            _billboardMesh = new Mesh(_device, Primitives.GenerateQuadForYBillboard());
        }

        public Mesh GetCubeMesh()
        {
            return _cubeMesh;
        }

        public Mesh GetBillboardMesh()
        {
            return _billboardMesh;
        }

        public ModelMesh GetMeshByFilePath(string filePath)
        {
            if (_meshCache.ContainsKey(filePath)) return _meshCache[filePath];

            var assimpContext = new Assimp.AssimpContext();
            var assimpScene = assimpContext.ImportFile(filePath, PostProcessSteps.GenerateNormals | PostProcessSteps.GenerateUVCoords | PostProcessSteps.Triangulate);

            if (assimpScene.MeshCount > 1) throw new NotSupportedException("Currently are just single meshs supported.");

            var assimpMesh = assimpScene.Meshes.First();

            var modelMesh = new ModelMesh
            {
                Mesh = new Mesh(_device, GenerateGeometryDataFromAssimpMesh(assimpMesh)),
                Material = GenerateMaterialFromMesh(assimpMesh.MaterialIndex, assimpScene)
            };
            _meshCache.Add(filePath, modelMesh);
            return modelMesh;

        }

        private Material GenerateMaterialFromMesh(int materialIndex, Assimp.Scene assimpScene)
        {
            //todo: extract texture and load from file. therefore, a texture manager should be used.
            var assimpMaterial = assimpScene.Materials[materialIndex];
            var material = new Material(null);
            return material;
        }

        private static GeometryData GenerateGeometryDataFromAssimpMesh(Assimp.Mesh mesh)
        {
            var geometryData = new GeometryData
            {
                Vertices = new VertexPositionNormalTexture[mesh.VertexCount],
                Indices = new ushort[mesh.FaceCount * 3]
            };

            geometryData.Vertices = new VertexPositionNormalTexture[mesh.VertexCount];

            for (var i = 0; i < mesh.VertexCount; i++)
            {
                var vertex = mesh.Vertices[i];
                geometryData.Vertices[i].Position = new Vector3(vertex.X, vertex.Y, vertex.Z);

                var normal = mesh.Normals[i];
                geometryData.Vertices[i].Normal = new Vector3(normal.X, normal.Y, normal.Z);

                var texcoord = mesh.TextureCoordinateChannels[0][i];
                geometryData.Vertices[i].TextureCoordinate = new Vector2(texcoord.X, texcoord.Y);
            }

            for (var i = 0; i < mesh.FaceCount; i++)
            {
                geometryData.Indices[i*3 + 0] = (ushort) mesh.Faces[i].Indices[0];
                geometryData.Indices[i*3 + 1] = (ushort) mesh.Faces[i].Indices[1];
                geometryData.Indices[i*3 + 2] = (ushort) mesh.Faces[i].Indices[2];
            }

            return geometryData;
        }
    }
}
