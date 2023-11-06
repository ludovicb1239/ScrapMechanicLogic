using CsharpVoxReader;
using CsharpVoxReader.Chunks;
using ScrapMechanicLogic;
using System;
using System.Diagnostics;
using System.Linq;

namespace ScrapMechanicLogic
{
    class MyVoxLoader : IVoxLoader
    {
        public List<Position> positions;
        public List<Bound> boundingBoxes;
        public string[] palette;
        public List<byte> paletteIndexes;
        bool roundColors = false;
        public MyVoxLoader(bool roundColors = false) {
            this.roundColors = roundColors;

            paletteIndexes = new();
            positions = new();
            boundingBoxes = new();
            palette = new string[0];
        }
        void IVoxLoader.LoadModel(int sizeX, int sizeY, int sizeZ, byte[,,] data)
        {
            paletteIndexes = new();
            positions = new();
            boundingBoxes = new();

            Console.WriteLine("X bounds : " + sizeX);
            Console.WriteLine("Y bounds : " + sizeY);
            Console.WriteLine("Z bounds : " + sizeZ);
            Bound boundingBox;
            byte dat;
            for (int z = 0; z < sizeZ; z++)
            {
                for (int y = 0; y < sizeY; y++)
                {
                    for (int x = 0; x < sizeX; x++)
                    {
                        if (data[x, y, z] != 0)
                        {
                            dat = data[x, y, z];
                            boundingBox = new Bound { x = 1, y = 1, z = 1 };
                            boundingBox.x = getLineLenght(x, y, z, dat, data, sizeX);
                            boundingBox.y = getDepthLenght(x, y, z, dat, data, boundingBox.x, sizeY);
                            boundingBox.z = getHeightLenght(x, y, z, dat, data, boundingBox.x, boundingBox.y, sizeZ);


                            paletteIndexes.Add(dat);
                            positions.Add(new Position() { x = x, y = y, z = z });
                            boundingBoxes.Add(boundingBox);

                            //Reset all of the bounded values
                            for (int z1 = z; z1 < boundingBox.z + z; z1++)
                            {
                                for (int y1 = y; y1 < boundingBox.y + y; y1++)
                                {
                                    for (int x1 = x; x1 < boundingBox.x + x; x1++)
                                    {
                                        data[x1, y1, z1] = 0;
                                    }
                                }
                            }
                        }
                    }
                }
            }

            Console.WriteLine(positions.Count + " big blocks");
        }
        int getLineLenght(int x, int y, int z, byte dat, byte[,,] data, int sizeX)
        {
            bool valid = true;
            int lenghtX = 0;
            //Alongate along the X
            while (valid)
            {
                lenghtX++;
                valid = (x + lenghtX == sizeX) ? false : (data[x + lenghtX, y, z] == dat);
            }
            return lenghtX;
        }
        bool isLineValid(int x, int y, int z, byte dat, byte[,,] data, int lenghtX)
        {
            for (int x1 = x; x1 < x + lenghtX; x1++)
            {
                if (dat != data[x1, y, z]) return false;
            }
            return true;
        }
        int getDepthLenght(int x, int y, int z, byte dat, byte[,,] data, int lenghtX, int sizeY)
        {
            bool valid = true;
            int lenghtY = 0;
            //Alongate along the Y
            while (valid)
            {
                lenghtY++;
                valid = (y + lenghtY == sizeY) ? false : isLineValid(x, y + lenghtY, z, dat, data, lenghtX);
            }
            return lenghtY;
        }
        bool isSurfaceValid(int x, int y, int z, byte dat, byte[,,] data, int lenghtX, int lenghtY)
        {
            for (int y1 = y; y1 < y + lenghtY; y1++)
            {
                if (!isLineValid(x, y1, z, dat, data, lenghtX))
                    return false;
            }
            return true;
        }
        int getHeightLenght(int x, int y, int z, byte dat, byte[,,] data, int lenghtX, int lenghtY, int sizeZ)
        {
            bool valid = true;
            int lenghtZ = 0;
            //Alongate along the Y
            while (valid)
            {
                lenghtZ++;
                valid = (z + lenghtZ == sizeZ) ? false : isSurfaceValid(x, y, z + lenghtZ, dat, data, lenghtX, lenghtY);
            }
            return lenghtZ;
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