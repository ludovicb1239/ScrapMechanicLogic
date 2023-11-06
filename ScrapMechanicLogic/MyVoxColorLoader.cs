using CsharpVoxReader;
using CsharpVoxReader.Chunks;
using ScrapMechanicLogic;
using System;
using System.Diagnostics;
using System.Linq;

namespace ScrapMechanicLogic
{
    class MyVoxColorLoader : IVoxLoader
    {
        bool roundColors = false;
        public string[] palette;
        public List<byte> usedIndexes;
        public MyVoxColorLoader(bool roundColors = false) {
            this.roundColors = roundColors;

            usedIndexes = new();
            palette = new string[0];
        }
        void IVoxLoader.LoadModel(int sizeX, int sizeY, int sizeZ, byte[,,] data)
        {
            usedIndexes = new();
            for (int z = 0; z < sizeZ; z++)
            {
                for (int y = 0; y < sizeY; y++)
                {
                    for (int x = 0; x < sizeX; x++)
                    {
                        if (data[x, y, z] != 0)
                        {
                            if (!usedIndexes.Contains(data[x, y, z]))
                                usedIndexes.Add(data[x, y, z]);
                        }
                    }
                }
            }
        }
        void IVoxLoader.LoadPalette(uint[] palette)
        {
            this.palette = new string[palette.Length];
            for (int i = 0; i < palette.Length; i++)
            {
                if (roundColors)
                    this.palette[i] = ScrapMechanicColor.CompressColor(ScrapMechanicColor.RGBToHex(palette[i]));
                else
                    this.palette[i] = ScrapMechanicColor.RGBToHex(palette[i]);
            }
        }

        void IVoxLoader.NewGroupNode(int id, Dictionary<string, byte[]> attributes, int[] childrenIds)
        {
            //throw new NotImplementedException();
        }

        void IVoxLoader.NewLayer(int id, string name, Dictionary<string, byte[]> attributes)
        {
            //throw new NotImplementedException();
        }

        void IVoxLoader.NewMaterial(int id, Dictionary<string, byte[]> attributes)
        {
            //throw new NotImplementedException();
        }

        void IVoxLoader.NewShapeNode(int id, Dictionary<string, byte[]> attributes, int[] modelIds, Dictionary<string, byte[]>[] modelsAttributes)
        {
            //throw new NotImplementedException();
        }

        void IVoxLoader.NewTransformNode(int id, int childNodeId, int layerId, string name, Dictionary<string, byte[]>[] framesAttributes)
        {
            //throw new NotImplementedException();
        }

        void IVoxLoader.SetMaterialOld(int paletteId, MaterialOld.MaterialTypes type, float weight, MaterialOld.PropertyBits property, float normalized)
        {
            //throw new NotImplementedException();
        }

        void IVoxLoader.SetModelCount(int count)
        {
            //throw new NotImplementedException();
        }
    }
}