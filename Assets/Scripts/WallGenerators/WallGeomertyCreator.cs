using Unity.Collections;
using Unity.Jobs;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.U2D;


[CreateAssetMenu(fileName = "SpriteShapeQuad", menuName = "ScriptableObjects/SpriteShapeQuad", order = 1)]
public class SpriteShapeQuad : SpriteShapeGeometryCreator
{
    public override int GetVertexArrayCount(SpriteShapeController sc)
    {
        // Set the maximum size required for the Vertices.
        return 64;
    }

    public override JobHandle MakeCreatorJob(
        SpriteShapeController sc,
        NativeArray<ushort> indices,
        NativeSlice<Vector3> positions,
        NativeSlice<Vector2> texCoords,
        NativeSlice<Vector4> tangents,
        NativeArray<SpriteShapeSegment> segments,
        NativeArray<float2> colliderData
    )
    {
        NativeArray<Bounds> bounds = sc.spriteShapeRenderer.GetBounds();
        var spline = sc.spline;
        int pointCount = spline.GetPointCount();
        Bounds bds = new Bounds(spline.GetPosition(0), spline.GetPosition(0));
        for (int i = 0; i < pointCount; ++i)
            bds.Encapsulate(spline.GetPosition(i));
        bounds[0] = bds;

        var cj = new CreatorJob()
        { indices = indices, positions = positions, texCoords = texCoords, segments = segments, bounds = bds };
        var jh = cj.Schedule();
        return jh;
    }
}

// A simple C# job to generate a quad.
public struct CreatorJob : IJob
{
    // Indices of the generated triangles.
    public NativeArray<ushort> indices;
    // Vertex positions.
    public NativeSlice<Vector3> positions;
    // Texture Coordinates.
    public NativeSlice<Vector2> texCoords;
    // Sub-meshes of generated geometry.
    public NativeArray<UnityEngine.U2D.SpriteShapeSegment> segments;
    // Input Bounds.
    public Bounds bounds;

    public void Execute()
    {
        // Generate Positions/TexCoords/Indices for the Quad.
        positions[0] = bounds.min;
        texCoords[0] = Vector2.zero;
        positions[1] = bounds.max;
        texCoords[1] = Vector2.one;
        positions[2] = new Vector3(bounds.min.x, bounds.max.y, 0);
        texCoords[2] = new Vector2(0, 1);
        positions[3] = new Vector3(bounds.max.x, bounds.min.y, 0);
        texCoords[3] = new Vector2(1, 0);
        indices[0] = indices[3] = 0;
        indices[1] = indices[4] = 1;
        indices[2] = 2;
        indices[5] = 3;

        // Set the only sub-mesh (quad)
        var seg = segments[0];
        seg.geomIndex = seg.spriteIndex = 0;
        seg.indexCount = 6;
        seg.vertexCount = 4;
        segments[0] = seg;

        // Reset other sub-meshes.
        seg.geomIndex = seg.indexCount = seg.spriteIndex = seg.vertexCount = 0;
        for (int i = 1; i < segments.Length; ++i)
            segments[i] = seg;
    }
}

