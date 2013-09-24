using System;
using System.Collections.Generic;
using System.Data.Objects;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;


namespace CMS.WebMVC
{
    #region ImageHelpers Class
    public static class ImageHelpers
    {
        #region UploadImageFor
        /// <summary>
        /// HTML Helper to allow you to upload Images and Preview it.
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <typeparam name="TProperty"></typeparam>
        /// <param name="helper"></param>
        /// <param name="expression"></param>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="cssclass"></param>
        /// <param name="alt"></param>
        /// <param name="imgId"></param>
        /// <param name="height"></param>
        /// <param name="width"></param>
        /// <returns>HTML String</returns>
        public static MvcHtmlString UploadImageFor<TModel, TProperty>(this HtmlHelper<TModel> helper,
            Expression<Func<TModel, TProperty>> expression, string id = null, string name = null,
            string cssclass = null, string alt = null, string imgId = null, string height = null, string width = null,
            bool thumbnail = false, string thumbAlt = null, string thumbimgId = null, string thumbHeight = null, string thumbWidth = null,
            string thumbName = null)
        {
            const string type = "file";

            if (thumbAlt == null)
            {
                thumbAlt = "thumbAlt";
            }

            if (thumbimgId == null)
            {
                thumbimgId = "thumbnail";

            }

            if (thumbHeight == null)
            {
                thumbHeight = "100px";
            }

            if (thumbWidth == null)
            {
                thumbWidth = "100px";
            }

            if (thumbName == null)
            {
                thumbName = "thumbnailImageLoad";
            }

            if (cssclass == null)
            {
                cssclass = "upload";
            }

            if (id == null)
            {
                id = "File1";
            }

            if (name == null)
            {
                name = "imageLoad2";
            }

            if (alt == null)
            {
                alt = "Preview";
            }

            if (imgId == null)
            {
                imgId = "imgThumbnail2";
            }

            if (height == null)
            {
                height = "126px";
            }

            if (width == null)
            {
                width = "126px";
            }


            ///Firstly we build the input tag.
            //--Add the CSS class.
            var fileBuilder = new TagBuilder("input");
            fileBuilder.AddCssClass(cssclass);
            //--Add the name.
            fileBuilder.MergeAttribute("name", name);
            //--Add the id.
            fileBuilder.MergeAttribute("id", id);
            //--Add the type.
            fileBuilder.MergeAttribute("type", type);

            ///Secondly we build the img tag.
            //--Add the alt.
            var imgBuilder = new TagBuilder("img");
            imgBuilder.MergeAttribute("alt", alt);
            //--Add the id.
            imgBuilder.MergeAttribute("id", imgId);
            //--Add the height and width.
            imgBuilder.MergeAttribute("height", height);
            imgBuilder.MergeAttribute("width", width);

            var thumbnailBuilder = new TagBuilder("img");
            if (thumbnail == true)
            {
                ///Thirdly we build the thumbnail img tag.
                //--Add the name.
                thumbnailBuilder.MergeAttribute("name", name);
                //--Add the alt.
                thumbnailBuilder.MergeAttribute("alt", thumbAlt);
                //--Add the id.
                thumbnailBuilder.MergeAttribute("id", thumbimgId);
                //--Add the height and width.
                thumbnailBuilder.MergeAttribute("height", thumbHeight);
                thumbnailBuilder.MergeAttribute("width", thumbWidth);
            }

            ///Merge the two together into a p tag.
            var mergedBuilder = new TagBuilder("div");
            mergedBuilder.AddCssClass("file-upload");
            mergedBuilder.InnerHtml += fileBuilder.ToString(TagRenderMode.SelfClosing);
            mergedBuilder.InnerHtml += imgBuilder.ToString(TagRenderMode.SelfClosing);

            if (thumbnail == true)
            {
                mergedBuilder.InnerHtml += thumbnailBuilder.ToString(TagRenderMode.SelfClosing);
            }

            return MvcHtmlString.Create(mergedBuilder.ToString());

        }
        #endregion

        #region  DisplayImageFor

        public static MvcHtmlString DisplayImage(string imagePath, string alt = null, string action = null, string controller = null,
           string actionParameterName = null, string height = null, string width = null, bool thumbnail = false, bool tableLink = false)
        {



            if (String.IsNullOrEmpty(height))
            {
                if (thumbnail == true)
                {
                    height = "100px";
                }
                else
                {
                    // height = "126px";
                }
            }

            if (String.IsNullOrEmpty(width))
            {
                if (thumbnail == true)
                {
                    width = "100px";
                }
                else
                {
                    //  width = "126px";
                }
            }

            if (String.IsNullOrEmpty(actionParameterName))
            {
                actionParameterName = "id";
            }
            if (String.IsNullOrEmpty(controller))
            {
                controller = "Image";
            }
            var imgBuilder = new TagBuilder("img");
            imgBuilder.MergeAttribute("alt", alt);
            imgBuilder.MergeAttribute("src", imagePath);
            if (!String.IsNullOrEmpty(height))
                imgBuilder.MergeAttribute("height", height);
            if (!String.IsNullOrEmpty(width))
                imgBuilder.MergeAttribute("width", width);
            return MvcHtmlString.Create(imgBuilder.ToString(TagRenderMode.SelfClosing));


        }

        public static MvcHtmlString DisplayImageFor<TModel, TProperty>(this HtmlHelper<TModel> helper,
            Expression<Func<TModel, TProperty>> expression, string alt = null, string action = null, string controller = null,
            string actionParameterName = null, string height = null, string width = null, bool thumbnail = false, bool tableLink = false)
        {

            if (String.IsNullOrEmpty(alt))
            {
                string _name = ExpressionHelper.GetExpressionText(expression);
                alt = helper.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldName(_name);
            }

            if (String.IsNullOrEmpty(height))
            {
                if (thumbnail == true)
                {
                    height = "100px";
                }
                else
                {
                    // height = "126px";
                }
            }

            if (String.IsNullOrEmpty(width))
            {
                if (thumbnail == true)
                {
                    width = "100px";
                }
                else
                {
                    //  width = "126px";
                }
            }

            if (String.IsNullOrEmpty(actionParameterName))
            {
                actionParameterName = "id";
            }

            ///---Set the default src settings if null
            ///--- src element is made up of action, controller and acionParameterName

            if (String.IsNullOrEmpty(action))
            {
                if (thumbnail == false)
                {
                    action = "GetImage";
                }
                else
                {
                    action = "GetThumbnailImage";
                }

            }

            if (String.IsNullOrEmpty(controller))
            {
                controller = "Image";
            }

            ModelMetadata metadata = ModelMetadata.FromLambdaExpression(expression, helper.ViewData);
            Object value = metadata.Model;
            Type valueType = null;
            if (value == null || string.IsNullOrEmpty(value.ToString()))
                return MvcHtmlString.Empty;

            valueType = metadata.Model.GetType();

            /*
            string name = ExpressionHelper.GetExpressionText(expression);
            string fullName = helper.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldName(name);
             */
            string src = string.Empty;
            if (ObjectValidation.IsStringType(valueType))
            {
                if (tableLink == true)
                {
                    value += "?tableLink=true";
                }
                src = value.ToString();
                //String.Format(CultureInfo.InvariantCulture, "/{0}/{1}/{2}", controller, action, value);
            }

            var imgBuilder = new TagBuilder("img");

            imgBuilder.MergeAttribute("alt", alt);
            imgBuilder.MergeAttribute("src", src);
            if (!String.IsNullOrEmpty(height))
                imgBuilder.MergeAttribute("height", height);
            if (!String.IsNullOrEmpty(width))
                imgBuilder.MergeAttribute("width", width);


            return MvcHtmlString.Create(imgBuilder.ToString(TagRenderMode.SelfClosing));


        }
        #endregion

        #region EditImageFor
        public static MvcHtmlString EditImageFor<TModel, TProperty>(this HtmlHelper<TModel> helper,
            Expression<Func<TModel, TProperty>> expression, string id = null, string name = null,
            string cssclass = null, string alt = null, string imgId = null, string height = null, string width = null,
            string action = null, string controller = null, bool tableLink = false)
        {
            const string type = "file";

            if (String.IsNullOrEmpty(cssclass))
            {
                cssclass = "upload";
            }

            if (String.IsNullOrEmpty(id))
            {
                id = "File1";
            }

            if (String.IsNullOrEmpty(name))
            {
                name = "imageLoad2";
            }

            if (String.IsNullOrEmpty(alt))
            {
                alt = "Preview";
            }

            if (String.IsNullOrEmpty(imgId))
            {
                imgId = "imgThumbnail2";
            }

            if (String.IsNullOrEmpty(height))
            {
                height = "126px";
            }

            if (String.IsNullOrEmpty(width))
            {
                width = "126px";
            }

            if (String.IsNullOrEmpty(action))
            {
                action = "GetImage";

            }

            if (String.IsNullOrEmpty(controller))
            {
                controller = "Image";
            }

            ModelMetadata metadata = ModelMetadata.FromLambdaExpression(expression, helper.ViewData);
            Object value = metadata.Model;
            Type valueType = null;
            if (value != null)
                valueType = metadata.Model.GetType();
            string src = "";
            if (ObjectValidation.IsStringType(valueType))
            {
                if (tableLink == true)
                {
                    value += "?tableLink=true";
                }

                src = value.ToString();
            }


            ///Firstly we build the input tag.
            //--Add the CSS class.
            var fileBuilder = new TagBuilder("input");
            fileBuilder.AddCssClass(cssclass);
            //--Add the name.
            fileBuilder.MergeAttribute("name", name);
            //--Add the id.
            fileBuilder.MergeAttribute("id", id);
            //--Add the type.
            fileBuilder.MergeAttribute("type", type);

            ///Secondly we build the img tag.
            //--Add the alt.
            var imgBuilder = new TagBuilder("img");
            imgBuilder.MergeAttribute("alt", alt);

            //--Add the name.
            imgBuilder.MergeAttribute("name", metadata.PropertyName);

            //--Add the name.
            imgBuilder.MergeAttribute("value", src);

            //--Add the id.
            imgBuilder.MergeAttribute("id", imgId);
            //--Add the height and width.
            imgBuilder.MergeAttribute("height", height);
            imgBuilder.MergeAttribute("width", width);
            //--Add src element.
            imgBuilder.MergeAttribute("src", src);
            var imgHiddenBuilder = new TagBuilder("input");
            //--Add the name.
            imgHiddenBuilder.MergeAttribute("name", metadata.PropertyName);
            //--Add the name.
            imgHiddenBuilder.MergeAttribute("value", src);
            //--Add the name.
            imgHiddenBuilder.MergeAttribute("type", "hidden");

            ///Merge the two together into a p tag.
            var mergedBuilder = new TagBuilder("div");
            mergedBuilder.AddCssClass("file-upload");
            mergedBuilder.InnerHtml += fileBuilder.ToString(TagRenderMode.SelfClosing);
            mergedBuilder.InnerHtml += imgBuilder.ToString(TagRenderMode.SelfClosing);
            mergedBuilder.InnerHtml += imgHiddenBuilder.ToString(TagRenderMode.SelfClosing);

            return MvcHtmlString.Create(mergedBuilder.ToString());

        }
        #endregion

    }
    #endregion


    #region class ObjectValidation
    /// <summary>
    /// This class allows me to build code to validate my what type of Objects im using.
    /// That way i can build specfic logic to cope with this
    /// </summary>
    public class ObjectValidation
    {

        public static bool IsNumericType(Type type)
        {
            if (type == null)
            {
                return false;
            }

            switch (Type.GetTypeCode(type))
            {
                case TypeCode.Byte:
                case TypeCode.Decimal:
                case TypeCode.Double:
                case TypeCode.Int16:
                case TypeCode.Int32:
                case TypeCode.Int64:
                case TypeCode.SByte:
                case TypeCode.Single:
                case TypeCode.UInt16:
                case TypeCode.UInt32:
                case TypeCode.UInt64:
                    return true;

                case TypeCode.Object:
                    if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>))
                    {
                        return IsNumericType(Nullable.GetUnderlyingType(type));
                    }
                    return false;

            }

            return false;
        }

        public static bool IsStringType(Type type)
        {
            if (type == null)
            {
                return false;
            }

            switch (Type.GetTypeCode(type))
            {
                case TypeCode.String:
                    return true;

                case TypeCode.Object:
                    if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>))
                    {
                        return IsStringType(Nullable.GetUnderlyingType(type));
                    }

                    return false;

            }

            return false;
        }
    }

    #endregion

    #region Debug Class
    /// <summary>
    /// Extension method to allow us to Debug various Web related areas.
    /// </summary>
    public static class Debug
    {
        #region ModelStateErrors
#if DEBUG
        /// <summary>
        /// Output the properties which are causing the issues when 
        /// the model is binding.
        /// </summary>
        public static void ModelStateErrors(ModelStateDictionary modelState)
        {
            var errors = modelState.Where(a => a.Value.Errors.Count > 0)
                .Select(b => new { b.Key, b.Value.Errors })
                .ToArray();

            foreach (var modelStateErrors in errors)
            {
                int count = modelStateErrors.Errors.Count();
                System.Diagnostics.Debug.WriteLine("...Errored When Binding.", modelStateErrors.Key.ToString());

            }

        }
#endif
        #endregion

    }
    #endregion

    #region Extension Methods
    public static class Extensions
    {
        #region ModelStateErrors
#if DEBUG
        /// <summary>
        /// Output the properties which are causing the issues when 
        /// the model is binding.
        /// </summary>
        public static void ModelStateErrors(this ModelStateDictionary modelState)
        {
            var errors = modelState.Where(a => a.Value.Errors.Count > 0)
                .Select(b => new { b.Key, b.Value.Errors })
                .ToArray();

            foreach (var modelStateErrors in errors)
            {
                int count = modelStateErrors.Errors.Count();
                System.Diagnostics.Debug.WriteLine("...Errored When Binding.", modelStateErrors.Key.ToString());

            }

        }
#endif
        #endregion


        #region CountAll
        public static int CountAll<TEntity>(this ObjectSet<TEntity> objectSet) where TEntity : class
        {
            int count = 0;
            foreach (var ObjectSet in objectSet)
            {
                if (objectSet != null)
                {
                    count++;
                }
            }

            return count;
        }
        #endregion





    }
    #endregion


    #region interface IImageModel
    /// <summary>
    /// IImageModel - Mandatory Signatures that we need
    /// </summary>
    public interface IImageModel
    {
        //-- ImageModel Property Signature.
        Guid UniqueId { get; set; }
        Guid TableLink { get; set; }
        String RecordStatus { get; set; }
        Byte[] Source { get; set; }
        Int32 FileSize { get; set; }
        String FileName { get; set; }
        String FileExtension { get; set; }
        String ContentType { get; set; }
    }
    #endregion

    #region class Images
    /// <summary>
    /// Static Utility to deal with common Image Stream tasks.
    /// </summary>
    public static class Images
    {
        /// <summary>
        /// Takes the inputstream and pushes that through a Image.FromStream
        /// to give the bytes needed to store in a Varbinary 
        /// </summary>
        /// <param name="stream">Stream</param>
        /// <returns>Binary File</returns>
        public static byte[] ImageToBinary(Stream stream)
        {
            byte[] file = null;

            //--Set the stream to the beginning before we start.

            using (Image img = Image.FromStream(stream))
            {

                //--Initalise the size of the array

                file = new byte[stream.Length];

                //--Create a new BinaryReader and set the InputStream for the Images InputStream to the
                //--beginning, as we create the img using a stream.
                BinaryReader reader = new BinaryReader(stream);
                Streams.RewindStream(ref stream);

                //--Load the image binary.
                return file = reader.ReadBytes((int)stream.Length);
            }

        }

        /// <summary>
        /// Takes an inputstream and returns an Image height.
        /// </summary>
        /// <param name="stream">Stream</param>
        /// <returns>Image Height</returns>
        public static int FromStreamHeight(Stream stream)
        {

            //--Rewind the memory back to the beginning of the stream.
            Streams.RewindStream(ref stream);

            using (Image img = Image.FromStream(stream))
            {

                return img.Height;
            }
        }

        #region FromStreamWidth
        /// <summary>
        /// Takes an inputstream and returns an Image width.
        /// </summary>
        /// <param name="stream">Stream</param>
        /// <returns>Image Width</returns>
        public static int FromStreamWidth(Stream stream)
        {

            //--Rewind the memory back to the beginning of the stream.
            Streams.RewindStream(ref stream);

            using (Image img = Image.FromStream(stream))
            {

                return img.Width;
            }
        }
        #endregion

        #region FromByteHeight
        /// <summary>
        /// Takes an byte array and returns an Image height.
        /// </summary>
        /// <param name="image">byte array of an Image.</param>
        /// <returns>Height</returns>
        public static int FromByteHeight(byte[] image)
        {
            Image img;
            int height = 0;

            if (image != null)
            {
                using (MemoryStream stream = new MemoryStream(image))
                {
                    img = Image.FromStream(stream);
                    height = img.Height;

                }
            }

            return height;

        }
        #endregion

        #region FromByteWidth
        /// <summary>
        /// Takes an byte array and returns an Image width.
        /// </summary>
        /// <param name="image">byte array of an Image.</param>
        /// <returns>Width</returns>
        public static int FromByteWidth(byte[] image)
        {
            Image img;
            int width = 0;

            if (image != null)
            {
                using (MemoryStream stream = new MemoryStream(image))
                {
                    img = Image.FromStream(stream);
                    width = img.Height;

                }
            }

            return width;

        }
        #endregion

        #region CreateThumbnailToByte
        /// <summary>
        /// This method creates a Thumbnail Image and and scales it. It returns a byte array
        /// to be used.
        /// </summary>
        /// <param name="stream">Image Stream</param>
        /// <param name="maxHeight">Max Height (Used to scale the image</param>
        /// <param name="maxWidth">Max Width (Used to scale the image)</param>
        /// <returns>Scaled thumbail image byte array.</returns>
        public static byte[] CreateThumbnailToByte(Stream stream, double maxHeight, double maxWidth)
        {
            int newHeight;
            int newWidth;
            double aspectRatio = 0;
            double boxRatio = 0;
            double scale = 0;

            Stream imageStream = new MemoryStream();
            Image originalImage;

            Streams.RewindStream(ref stream);
            using (originalImage = Image.FromStream(stream))
            {
                //--We need too maintain the aspect ratio on the image.
                aspectRatio = originalImage.Width / originalImage.Height;
                boxRatio = maxWidth / maxHeight;

                if (boxRatio > aspectRatio)
                {
                    scale = maxHeight / originalImage.Height;
                }
                else
                {
                    scale = maxWidth / originalImage.Width;
                }

                //--Scale the Original Images dimensions
                newHeight = (int)(originalImage.Height * scale);
                newWidth = (int)(originalImage.Width * scale);

                using (var bitmap = new Bitmap(newWidth, newHeight))

                //--Create a new GDI+ drawing surfance based on the original Image. This methid allows us to alter
                //--it where necessary, based on advice from here. http://nathanaeljones.com/163/20-image-resizing-pitfalls/
                using (var graphics = Graphics.FromImage(bitmap))
                {
                    var rectangle = new Rectangle(0, 0, newWidth, newHeight);

                    graphics.CompositingQuality = CompositingQuality.HighQuality;
                    graphics.SmoothingMode = SmoothingMode.HighQuality;
                    graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
                    graphics.DrawImage(originalImage, rectangle);

                    //--The same image to a new ImageStream so we can convert this to a byte array.
                    bitmap.Save(imageStream, originalImage.RawFormat);

                }

                byte[] thumbnail = new byte[imageStream.Length];
                BinaryReader reader = new BinaryReader(imageStream);
                imageStream.Seek(0, SeekOrigin.Begin);

                //--Load the image binary.
                thumbnail = reader.ReadBytes((int)imageStream.Length);
                return thumbnail;

            }

        }
        #endregion

        #region CreateThumbnailToImage
        /// <summary>
        /// This method creates a Thumbnail Image and and scales it. It returns a Image
        /// to be used.
        /// </summary>
        /// <param name="stream">Image Stream</param>
        /// <param name="maxHeight">Max Height (Used to scale the image</param>
        /// <param name="maxWidth">Max Width (Used to scale the image)</param>
        /// <returns>Scaled thumbail Image.</returns>
        public static Image CreateThumbnailToImage(Stream stream, TypePathImage typeImg)
        {


            double maxHeight = StaticHelper.heightSmallImg, maxWidth = StaticHelper.WidthSmallImg;
            switch (typeImg)
            {
                case TypePathImage.Large:
                    maxHeight = StaticHelper.heightLargeImg;
                    maxWidth = StaticHelper.WidthLargeImg;
                    break;
                case TypePathImage.Small:
                    maxHeight = StaticHelper.heightSmallImg;
                    maxWidth = StaticHelper.WidthSmallImg;
                    break;
                case TypePathImage.Thumbnail:
                    maxHeight = StaticHelper.heightThumbImg;
                    maxWidth = StaticHelper.WidthThumbImg;
                    break;
            }

            int newHeight;
            int newWidth;
            double aspectRatio = 0;
            double boxRatio = 0;
            double scale = 0;
            Stream imageStream = new MemoryStream();
            Image originalImage;
            using (originalImage = Image.FromStream(stream))
            {
                //--We need too maintain the aspect ratio on the image.
                aspectRatio = originalImage.Width / originalImage.Height;
                boxRatio = maxWidth / maxHeight;

                if (boxRatio > aspectRatio)
                {
                    scale = maxHeight / originalImage.Height;
                }
                else
                {
                    scale = maxWidth / originalImage.Width;
                }

                //--Scale the Original Images dimensions
                newHeight = (int)(originalImage.Height * scale);
                newWidth = (int)(originalImage.Width * scale);




                using (var bitmap = new Bitmap(newWidth, newHeight))

                //--Create a new GDI+ drawing surfance based on the original Image. This methid allows us to alter
                //--it where necessary, based on advice from here. http://nathanaeljones.com/163/20-image-resizing-pitfalls/
                using (var graphics = Graphics.FromImage(bitmap))
                {
                    var rectangle = new Rectangle(0, 0, newWidth, newHeight);

                    graphics.CompositingQuality = CompositingQuality.HighQuality;
                    graphics.SmoothingMode = SmoothingMode.HighQuality;
                    graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
                    graphics.DrawImage(originalImage, rectangle);


                    AddWatermark(graphics, "Copyright CMS", new Point(newWidth, newHeight));
                    //--The same image to a new ImageStream so we can convert this to a byte array.
                    bitmap.Save(imageStream, originalImage.RawFormat);

                }
            }

            return Image.FromStream(imageStream);

        }
        #endregion

        public static void AddWatermark(Graphics gr, string watermarkText, Point pt)
        {

            if (pt.X > 400 || pt.Y > 270)
            {
                Font font = null;
                if (pt.X > 1270 || pt.Y > 850)
                {
                    pt.X = pt.X - 200;
                    pt.Y = pt.Y - 30;
                    font = new Font("Arial", 18, FontStyle.Bold, GraphicsUnit.Pixel);
                }
                else
                {
                    pt.X = pt.X - 100;
                    pt.Y = pt.Y - 30;
                    font = new Font("Arial", 12, FontStyle.Bold, GraphicsUnit.Pixel);

                }
                //Adds a transparent watermark with an 100 alpha value.
                SolidBrush transBrush = new SolidBrush(Color.FromArgb(100, 0, 0, 0));
                //Color color = Color.FromArgb(100, 0, 0, 0);
                //The position where to draw the watermark on the image
                gr.DrawString(watermarkText, font, transBrush, pt);

            }
        }

    }

    #endregion

    #region Streams
    /// <summary>
    /// Static Utility to deal with common Stream task.
    /// </summary>
    public static class Streams
    {
        /// <summary>
        /// Static method to handle returning the offset of a stream to zero (Beginning)
        /// </summary>
        /// <param name="stream">ref Stream</param>
        public static void RewindStream(ref Stream stream)
        {
            if (stream.CanSeek)
            {
                //--If the stream can seek, return the offset back to the beginning using the correct Seek Method.
                stream.Seek(0, SeekOrigin.Begin);

            }
            else
            {
                //--Store the InputStream in memory which will return it to the beginning
                //--NB: THis is done within a "using" statement so the relevant Dispose 
                //--methods will be handled.
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    //--Create a memory buffer.
                    byte[] buffer = new byte[stream.Length];
                    int bytesRead;

                    //--Read the input stream into memory.
                    while ((bytesRead = stream.Read(buffer, 0, buffer.Length)) > 0)
                    {

                        memoryStream.Write(buffer, 0, bytesRead);
                    }

                    //--Reset the position to the beginning of the stream.
                    memoryStream.Position = 0;

                    //--Re-write the memoryStream back into the input stream.
                    byte[] memoryStreamBuffer = new byte[memoryStream.Length];
                    while ((bytesRead = memoryStream.Read(memoryStreamBuffer, 0, (int)memoryStream.Length)) > 0)
                    {
                        stream.Write(memoryStreamBuffer, 0, bytesRead);

                    }

                }
            }

        }

    }
    #endregion
}