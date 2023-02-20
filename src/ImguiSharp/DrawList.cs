namespace ImguiSharp
{
    public readonly unsafe struct DrawList : INativeReferenceWrapper<DrawList, Native.ImDrawList>
    {
        private readonly Native.ImDrawList* _list;

        public ReferenceVector<Native.ImDrawCmd, DrawCommand> Commands => new(_list->CmdBuffer.Data, _list->CmdBuffer.Size);

        public ValueVector<Native.ImDrawIdx, DrawIndex> Indexes => new(_list->IdxBuffer.Data, _list->IdxBuffer.Size);

        public ValueVector<Native.ImDrawVert, DrawVertex> Vertices => new(_list->VtxBuffer.Data, _list->VtxBuffer.Size);

        public DrawListOptions Options => (DrawListOptions)_list->Flags;

        public Rectangle ClipRectangle => new(Position.Wrap(Native.ImDrawList_GetClipRectMin(_list)), Position.Wrap(Native.ImDrawList_GetClipRectMax(_list)));

        private DrawList(Native.ImDrawList* list)
        {
            _list = list;
        }

        public void PushClipRectangle(Rectangle rect, bool intersectWithCurrentClipRectangle = false) => Native.ImDrawList_PushClipRect(_list, rect.Min.ToNative(), rect.Max.ToNative(), intersectWithCurrentClipRectangle);

        public void PushClipRectFullScreen() => Native.ImDrawList_PushClipRectFullScreen(_list);

        public void PopClipRect() => Native.ImDrawList_PopClipRect(_list);

        public void PushTextureID(TextureId textureId) => Native.ImDrawList_PushTextureID(_list, textureId.ToNative());

        public void PopTextureID() => Native.ImDrawList_PopTextureID(_list);

        public void AddLine(Position p1, Position p2, uint color, float thickness = 1.0f) => Native.ImDrawList_AddLineEx(_list, p1.ToNative(), p2.ToNative(), color, thickness);

        public void AddRect(Rectangle rect, uint color, float rounding = default, DrawOptions options = default, float thickness = 1.0f) => Native.ImDrawList_AddRectEx(_list, rect.Min.ToNative(), rect.Max.ToNative(), color, rounding, (Native.ImDrawFlags)options, thickness);

        public void AddRectFilled(Rectangle rect, uint color, float rounding = default, DrawOptions options = default) => Native.ImDrawList_AddRectFilledEx(_list, rect.Min.ToNative(), rect.Max.ToNative(), color, rounding, (Native.ImDrawFlags)options);

        public void AddRectFilledMultiColor(Rectangle rect, uint colorUpperLeft, uint colorUpperRight, uint colorBottomRight, uint colorBottomLeft) => Native.ImDrawList_AddRectFilledMultiColor(_list, rect.Min.ToNative(), rect.Max.ToNative(), colorUpperLeft, colorUpperRight, colorBottomRight, colorBottomLeft);

        public void AddQuad(Position p1, Position p2, Position p3, Position p4, uint color, float thickness = 1.0f) => Native.ImDrawList_AddQuadEx(_list, p1.ToNative(), p2.ToNative(), p3.ToNative(), p4.ToNative(), color, thickness);

        public void AddQuadFilled(Position p1, Position p2, Position p3, Position p4, uint color) => Native.ImDrawList_AddQuadFilled(_list, p1.ToNative(), p2.ToNative(), p3.ToNative(), p4.ToNative(), color);

        public void AddTriangle(Position p1, Position p2, Position p3, uint color, float thickness = 1.0f) => Native.ImDrawList_AddTriangleEx(_list, p1.ToNative(), p2.ToNative(), p3.ToNative(), color, thickness);

        public void AddTriangleFilled(Position p1, Position p2, Position p3, uint color) => Native.ImDrawList_AddTriangleFilled(_list, p1.ToNative(), p2.ToNative(), p3.ToNative(), color);

        public void AddCircle(Position center, float radius, uint color, int numberSegments = default, float thickness = 1.0f) => Native.ImDrawList_AddCircleEx(_list, center.ToNative(), radius, color, numberSegments, thickness);

        public void AddCircleFilled(Position center, float radius, uint color, int numberSegments = default) => Native.ImDrawList_AddCircleFilled(_list, center.ToNative(), radius, color, numberSegments);

        public void AddNgon(Position center, float radius, uint color, int numberSegments, float thickness = 1.0f) => Native.ImDrawList_AddNgonEx(_list, center.ToNative(), radius, color, numberSegments, thickness);

        public void AddNgonFilled(Position center, float radius, uint color, int numberSegments) => Native.ImDrawList_AddNgonFilled(_list, center.ToNative(), radius, color, numberSegments);

        public void AddText(Position position, uint color, string text)
        {
            fixed (byte* textPtr = Native.StringToUtf8(text))
            {
                Native.ImDrawList_AddText(_list, position.ToNative(), color, textPtr);
            }
        }

        public void AddTextImFontPtr(Font font, float fontSize, Position position, uint color, string text, float wrapWidth = default, Rectangle cpuFineClipRectange = default)
        {
            fixed (byte* textPtr = Native.StringToUtf8(text))
            {
                var rect = cpuFineClipRectange.ToNative();
                Native.ImDrawList_AddTextImFontPtrEx(_list, font.ToNative(), fontSize, position.ToNative(), color, textPtr, null, wrapWidth, &rect);
            }
        }

        public void AddPolyline(Span<Position> points, uint color, DrawOptions options, float thickness)
        {
            fixed (Position* pointsPtr = points)
            {
                Native.ImDrawList_AddPolyline(_list, (Native.ImVec2*)pointsPtr, points.Length, color, (Native.ImDrawFlags)options, thickness);
            }
        }

        public void AddConvexPolyFilled(Span<Position> points, uint color)
        {
            fixed (Position* pointsPtr = points)
            {
                Native.ImDrawList_AddConvexPolyFilled(_list, (Native.ImVec2*)pointsPtr, points.Length, color);
            }
        }

        public void AddBezierCubic(Position p1, Position p2, Position p3, Position p4, uint color, float thickness, int numberSegments = default) => Native.ImDrawList_AddBezierCubic(_list, p1.ToNative(), p2.ToNative(), p3.ToNative(), p4.ToNative(), color, thickness, numberSegments);

        public void AddBezierQuadratic(Position p1, Position p2, Position p3, uint color, float thickness, int numberSegments = default) => Native.ImDrawList_AddBezierQuadratic(_list, p1.ToNative(), p2.ToNative(), p3.ToNative(), color, thickness, numberSegments);

        public void AddImage(TextureId userTextureId, Rectangle rectangle) => AddImage(userTextureId, rectangle, new(new(), new(1, 1)));

        public void AddImage(TextureId userTextureId, Rectangle rectangle, TextureRectangle textureRectangle, uint color = 0xFFFFFFFF) => Native.ImDrawList_AddImageEx(_list, userTextureId.ToNative(), rectangle.Min.ToNative(), rectangle.Max.ToNative(), textureRectangle.Min.ToNative(), textureRectangle.Max.ToNative(), color);

        public void AddImageQuad(TextureId userTextureId, Rectangle rectangle1, Rectangle rectangle2) => AddImageQuad(userTextureId, rectangle1, rectangle2, new(new(), new(1, 0)));

        public void AddImageQuad(TextureId userTextureId, Rectangle rectangle1, Rectangle rectangle2, TextureRectangle textureRectangle1) => AddImageQuad(userTextureId, rectangle1, rectangle2, textureRectangle1, new(new(1, 1), new(0, 1)));

        public void AddImageQuad(TextureId userTextureId, Rectangle rectangle1, Rectangle rectangle2, TextureRectangle textureRectangle1, TextureRectangle textureRectangle2, uint color = 0xFFFFFFFF) => Native.ImDrawList_AddImageQuadEx(_list, userTextureId.ToNative(), rectangle1.Min.ToNative(), rectangle1.Max.ToNative(), rectangle2.Min.ToNative(), rectangle2.Max.ToNative(), textureRectangle1.Min.ToNative(), textureRectangle1.Max.ToNative(), textureRectangle2.Min.ToNative(), textureRectangle2.Max.ToNative(), color);

        public void AddImageRounded(TextureId userTextureId, Rectangle rect, TextureRectangle uvRectangle, uint color, float rounding, DrawOptions options = default) => Native.ImDrawList_AddImageRounded(_list, userTextureId.ToNative(), rect.Min.ToNative(), rect.Max.ToNative(), uvRectangle.Min.ToNative(), uvRectangle.Max.ToNative(), color, rounding, (Native.ImDrawFlags)options);

        public void PathClear() => Native.ImDrawList_PathClear(_list);

        public void PathLineTo(Position position) => Native.ImDrawList_PathLineTo(_list, position.ToNative());

        public void PathLineToMergeDuplicate(Position position) => Native.ImDrawList_PathLineToMergeDuplicate(_list, position.ToNative());

        public void PathFillConvex(uint color) => Native.ImDrawList_PathFillConvex(_list, color);

        public void PathStroke(uint color, DrawOptions options = default, float thickness = 1.0f) => Native.ImDrawList_PathStroke(_list, color, (Native.ImDrawFlags)options, thickness);

        public void PathArcTo(Position center, float radius, float arcMin, float arcMax, int numberSegments = default) => Native.ImDrawList_PathArcTo(_list, center.ToNative(), radius, arcMin, arcMax, numberSegments);

        public void PathArcToFast(Position center, float radius, int arcMinOf12, int arcMaxOf12) => Native.ImDrawList_PathArcToFast(_list, center.ToNative(), radius, arcMinOf12, arcMaxOf12);

        public void PathBezierCubicCurveTo(Position p2, Position p3, Position p4, int numberSegments = default) => Native.ImDrawList_PathBezierCubicCurveTo(_list, p2.ToNative(), p3.ToNative(), p4.ToNative(), numberSegments);

        public void PathBezierQuadraticCurveTo(Position p2, Position p3, int numberSegments = default) => Native.ImDrawList_PathBezierQuadraticCurveTo(_list, p2.ToNative(), p3.ToNative(), numberSegments);

        public void PathRect(Rectangle rect, float rounding = default, DrawOptions options = default) => Native.ImDrawList_PathRect(_list, rect.Min.ToNative(), rect.Max.ToNative(), rounding, (Native.ImDrawFlags)options);

        public void AddCallback(Action<DrawList, DrawCommand> callback)
        {
            Imgui.DrawListCallbacks[(nuint)callback.GetHashCode()] = callback;
            Native.ImDrawList_AddCallback(_list, (delegate* unmanaged[Cdecl]<Native.ImDrawList*, Native.ImDrawCmd*, void>)unchecked((nuint)(-2)), (void*)callback.GetHashCode());
        }

        public void AddDrawCmd() => Native.ImDrawList_AddDrawCmd(_list);

        public DrawList? CloneOutput() => Wrap(Native.ImDrawList_CloneOutput(_list));

        public void ChannelsSplit(int count) => Native.ImDrawList_ChannelsSplit(_list, count);

        public void ChannelsMerge() => Native.ImDrawList_ChannelsMerge(_list);

        public void ChannelsSetCurrent(int n) => Native.ImDrawList_ChannelsSetCurrent(_list, n);

        public void PrimReserve(int indexCount, int vertexCount) => Native.ImDrawList_PrimReserve(_list, indexCount, vertexCount);

        public void PrimUnreserve(int indexCount, int vertexCount) => Native.ImDrawList_PrimUnreserve(_list, indexCount, vertexCount);

        public void PrimRect(Rectangle rectangle, uint color) => Native.ImDrawList_PrimRect(_list, rectangle.Min.ToNative(), rectangle.Max.ToNative(), color);

        public void PrimRectUV(Rectangle rectangle, TextureRectangle uvRectangle, uint color) => Native.ImDrawList_PrimRectUV(_list, rectangle.Min.ToNative(), rectangle.Max.ToNative(), uvRectangle.Min.ToNative(), uvRectangle.Max.ToNative(), color);

        public void PrimQuadUV(Rectangle rectangle1, Rectangle rectangle2, TextureRectangle textureRectangle1, TextureRectangle textureRectangle2, uint color) => Native.ImDrawList_PrimQuadUV(_list, rectangle1.Min.ToNative(), rectangle1.Max.ToNative(), rectangle2.Min.ToNative(), rectangle2.Max.ToNative(), textureRectangle1.Min.ToNative(), textureRectangle1.Max.ToNative(), textureRectangle2.Min.ToNative(), textureRectangle2.Max.ToNative(), color);

        public void PrimWriteVertex(Position position, TextureCoordinate uv, uint color) => Native.ImDrawList_PrimWriteVtx(_list, position.ToNative(), uv.ToNative(), color);

        public void PrimWriteIndex(DrawIndex index) => Native.ImDrawList_PrimWriteIdx(_list, index.ToNative());

        public void PrimVertex(Position position, TextureCoordinate uv, uint color) => Native.ImDrawList_PrimVtx(_list, position.ToNative(), uv.ToNative(), color);

        public static DrawList? Wrap(Native.ImDrawList* native) => native == null ? null : new(native);

        public Native.ImDrawList* ToNative() => _list;
    }
}
