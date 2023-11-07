using CsharpVoxReader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using CsharpVoxReader.Chunks;

namespace ScrapMechanicLogic
{
    public enum Orientation
    {
        Horizontal,Vertical
    }
    internal class MyImageLoader
    {
        public List<Position> positions;
        public List<string> colors;
        public List<Bound> bounds;
        bool compressColors = false;
        Orientation orientation = Orientation.Horizontal;
        bool dithering = false;
        int scaleDownFactor = 1;
        public MyImageLoader(bool compressColors = false, Orientation orientation = Orientation.Horizontal, bool dithering = false, int scaleDownFactor = 1)
        {
            this.compressColors = compressColors;
            this.orientation = orientation;
            this.dithering = dithering && compressColors;
            this.scaleDownFactor = scaleDownFactor;
            positions = new();
            colors = new();
            bounds = new();
        }
        public void LoadImage(string path)
        {
            Bitmap bitmap = new Bitmap(path);

            int width = bitmap.Width, height = bitmap.Height;
            Console.WriteLine("width : " + width);
            Console.WriteLine("height : " + height);

            if (scaleDownFactor != 1)
            {
                bitmap = ScaleDownImage(bitmap, scaleDownFactor);
                width = bitmap.Width; height = bitmap.Height;
                Console.WriteLine("scaled down width : " + width);
                Console.WriteLine("scaled down height : " + height);
            }

            if (dithering)
                ApplyFloydSteinbergDithering(bitmap);

            positions = new();
            colors = new();
            bounds = new();

            int lenght;

            for (int y = 0; y < height; y++)
            {
                lenght = 1;
                for (int x = 0; x < width; x++)
                {
                    Color pixelColor = bitmap.GetPixel(x, y);

                    if (pixelColor.A == 255)
                    {
                        if (compressColors)
                        {
                            string pixelColorHex = ScrapMechanicColor.CompressColor(ScrapMechanicColor.RGBToHex(pixelColor));
                            string nextPixelColorHex = (x == width - 1) ? string.Empty : ScrapMechanicColor.CompressColor(ScrapMechanicColor.RGBToHex(bitmap.GetPixel(x+1, y)));
                            if (pixelColorHex == nextPixelColorHex)
                            {
                                lenght++;
                            }
                            else
                            {
                                bounds.Add(new Bound() { x = lenght, y = 1, z = 1 });
                                addPos(x, y);
                                colors.Add(pixelColorHex);
                                lenght = 1;
                            }
                        }
                        else
                        {
                            Color nextPixelColor = (x == width - 1) ? Color.Empty : bitmap.GetPixel(x+1, y);
                            if (Color.Equals(pixelColor, nextPixelColor))
                            {
                                lenght++;
                            }
                            else
                            {
                                bounds.Add(new Bound() { x = lenght, y = 1, z = 1 });
                                addPos(x, y);
                                colors.Add(ScrapMechanicColor.RGBToHex(pixelColor));
                                lenght = 1;
                            }
                        }

                    }
                    else
                    {
                        lenght = 1;
                    }
                }
            }
            bitmap.Dispose(); // Don't forget to dispose the bitmap after you're done with it
            Console.WriteLine(positions.Count + " big blocks");
        }
        void addPos(int x, int y)
        {
            switch (orientation)
            {
                case Orientation.Horizontal:
                    positions.Add(new Position() { x = -x, y = y, z = 0 });
                    break;
                case Orientation.Vertical:
                    positions.Add(new Position() { x = -x, y = 0, z = -y });
                    break;
                default:
                    positions.Add(new Position() { x = -x, y = y, z = 0 });
                    break;
            }
        }

        public static Bitmap ScaleDownImage(Bitmap originalImage, int scaleFactor)
        {
            int newWidth = originalImage.Width / scaleFactor;
            int newHeight = originalImage.Height / scaleFactor;

            Bitmap scaledImage = new Bitmap(newWidth, newHeight);

            for (int y = 0; y < newHeight; y++)
            {
                for (int x = 0; x < newWidth; x++)
                {
                    int totalR = 0, totalG = 0, totalB = 0, totalA = 0;
                    int sampleCount = 0;

                    // Iterate through the pixels in the original image corresponding to the scaled pixel
                    for (int dy = 0; dy < scaleFactor; dy++)
                    {
                        for (int dx = 0; dx < scaleFactor; dx++)
                        {
                            int pixelX = x * scaleFactor + dx;
                            int pixelY = y * scaleFactor + dy;

                            if (pixelX < originalImage.Width && pixelY < originalImage.Height)
                            {
                                Color pixelColor = originalImage.GetPixel(pixelX, pixelY);
                                totalR += pixelColor.R;
                                totalG += pixelColor.G;
                                totalB += pixelColor.B;
                                totalA += pixelColor.A;
                                sampleCount++;
                            }
                        }
                    }

                    // Average the sampled colors
                    int avgR = totalR / sampleCount;
                    int avgG = totalG / sampleCount;
                    int avgB = totalB / sampleCount;
                    int avgA = totalA / sampleCount;

                    // Set the color of the scaled image pixel
                    Color scaledColor = Color.FromArgb(avgA > 128 ? 255 : 0, avgR, avgG, avgB);
                    scaledImage.SetPixel(x, y, scaledColor);
                }
            }
            return scaledImage;
        }
        public static void ApplyFloydSteinbergDithering(Bitmap inputImage)
        {
            int width = inputImage.Width;
            int height = inputImage.Height;

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    Color oldColor = inputImage.GetPixel(x, y);
                    Color newColor = GetClosestColor(oldColor);

                    inputImage.SetPixel(x, y, newColor);

                    int quantErrorR = oldColor.R - newColor.R;
                    int quantErrorG = oldColor.G - newColor.G;
                    int quantErrorB = oldColor.B - newColor.B;

                    // Distribute the error to neighboring pixels
                    if (x + 1 < width)
                        PropagateError(inputImage, x + 1, y, quantErrorR, quantErrorG, quantErrorB, 7.0 / 16.0);
                    if (x - 1 >= 0 && y + 1 < height)
                        PropagateError(inputImage, x - 1, y + 1, quantErrorR, quantErrorG, quantErrorB, 3.0 / 16.0);
                    if (y + 1 < height)
                        PropagateError(inputImage, x, y + 1, quantErrorR, quantErrorG, quantErrorB, 5.0 / 16.0);
                    if (x + 1 < width && y + 1 < height)
                        PropagateError(inputImage, x + 1, y + 1, quantErrorR, quantErrorG, quantErrorB, 1.0 / 16.0);
                }
            }
        }

        private static void PropagateError(Bitmap image, int x, int y, int quantErrorR, int quantErrorG, int quantErrorB, double weight)
        {
            Color currentColor = image.GetPixel(x, y);
            if (currentColor.A == 255)
            {
                int newR = Clamp(currentColor.R + (int)(quantErrorR * weight));
                int newG = Clamp(currentColor.G + (int)(quantErrorG * weight));
                int newB = Clamp(currentColor.B + (int)(quantErrorB * weight));
                image.SetPixel(x, y, Color.FromArgb(newR, newG, newB));
            }
            else
            {
                image.SetPixel(x, y, Color.FromArgb(0,0,0,0));
            }
        }

        private static int Clamp(int value)
        {
            return Math.Max(0, Math.Min(255, value));
        }

        private static Color GetClosestColor(Color inputColor)
        {
            if (inputColor.A == 255)
                return ScrapMechanicColor.CompressColor(inputColor);
            else
                return Color.FromArgb(0, 0, 0, 0);
        }
    }
}
