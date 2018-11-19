using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyCodeSolution.CommonClassLibrary;

namespace MyCodeSolution.PictureProcessing.Controllers
{
    public class UploadController : Controller
    {
        // GET: Upload
        public ActionResult Index()
        {
            return View();
        }


        public ContentResult UploadOneImage(int minwidth = 360, int minheight = 360)
        {
            JsonResult result = new JsonResult();

            HttpPostedFileBase file = HttpContext.Request.Files["fileUploadImage"];

            try
            {
                //获取前台的FILE  
                if (CheckFile(file))
                {
                    string ImageTrueName = System.IO.Path.GetFileName(file.FileName);
                    string imgExt = System.IO.Path.GetExtension(ImageTrueName);
                    string ImageName = Guid.NewGuid().ToString() + imgExt;
                    string TemporaryFilesDiskPath = Server.MapPath("~/") + "UploadFolder/Temp/";
                    try
                    {
                        file.SaveAs(TemporaryFilesDiskPath + ImageName);
                    }
                    catch (Exception ex) { }


                    System.Drawing.Image image = System.Drawing.Image.FromFile(TemporaryFilesDiskPath + ImageName);
                    int imgWidth = image.Width;
                    int imgHeight = image.Height;

                    #region 生成缩略图

                    double rate = 1.0 * imgWidth / imgHeight;
                    string BreviaryImageName = Guid.NewGuid().ToString() + imgExt;
                    if (imgWidth > 720)
                    {
                        imgWidth = 720;
                        imgHeight = (int)(imgWidth / rate);
                        ImageHelper.Breviary(TemporaryFilesDiskPath + ImageName, TemporaryFilesDiskPath + BreviaryImageName, imgWidth, imgHeight);
                    }
                    else
                    {
                        BreviaryImageName = ImageName;
                    }
                    #endregion

                    #region 处理窄图片
                    bool isWater = false;
                    if (imgWidth < minwidth)
                    {
                        isWater = true;
                        imgWidth = minwidth;
                    }
                    if (imgHeight < minheight)
                    {
                        isWater = true;
                        imgHeight = minheight;
                    }
                    string WaterImageName = Guid.NewGuid().ToString() + imgExt;
                    if (isWater)
                    {
                        ImageHelper.PicWaterMark(TemporaryFilesDiskPath + BreviaryImageName, TemporaryFilesDiskPath + WaterImageName, imgWidth, imgHeight);
                    }
                    else
                    {
                        WaterImageName = BreviaryImageName;
                    }
                    #endregion

                    //result.Data = new { statusCode = 200, message = "上传成功", ImgName = WaterImageName };     
                    return Content(WaterImageName);
                }
                else
                {
                    return Content("文件格式不正确");
                    //result.Data = new { statusCode = 500, message = "文件格式不正确" };
                }
            }
            catch
            {
                return Content("上传失败");
                //result.Data = new { statusCode = 500, message = "上传失败" };
            }

            //result.Data = new { statusCode = 200, message = "上传成功" };

            //return result;
        }


        /// <summary>
        /// 图片验证
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public bool CheckFile(HttpPostedFileBase HPF)
        {
            //验证有无选择文件
            if (HPF == null)
            {
                //Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Script", "<script>alert('请先选择图片！');</script>");
                return false;
            }

            //验证文件大小
            if (HPF.ContentLength == 0)
            {
                //Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Script", "<script>alert('不能上传大小是0的图片！');</script>");
                return false;
            }
            if (HPF.ContentLength >= 10485760)//10M
            {
                //Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Script", "<script>alert('超出上传图片限制大小，最大只能上传1M！');</script>");
                return false;
            }

            string imgExtention = System.IO.Path.GetExtension(HPF.FileName).ToLower();
            if (imgExtention != ".jpg" && imgExtention != ".jpe" && imgExtention != ".jpeg" && imgExtention != ".gif" && imgExtention != ".png" && imgExtention != ".bmp")
            {
                //Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Script", "<script>window.alert('原图片文件格式不正确,支持的格式有[ .jpg|.jpe|.jpeg|.png|.bmp|.gif ]！'); </script>");
                return false;
            }

            return true;
        }





        public ActionResult UploadImage()
        {
            return View();
        }

        [HttpPost]
        public JsonResult UpImage()
        {
            JsonResult result = new JsonResult();

            HttpPostedFileBase file = HttpContext.Request.Files["imagefile"];

            try
            {
                if (CheckFile(file))
                {
                    string ImageTrueName = System.IO.Path.GetFileName(file.FileName);
                    string imgExt = System.IO.Path.GetExtension(ImageTrueName);
                    string ImageName = Guid.NewGuid().ToString() + imgExt;
                    string TemporaryFilesDiskPath = Server.MapPath("~/") + "UploadFolder/";
                    try
                    {
                        file.SaveAs(TemporaryFilesDiskPath + ImageName);
                        result.Data = new { statusCode = 200, message = "上传成功", imgName = ImageName };
                    }
                    catch (Exception ex)
                    {
                        result.Data = new { statusCode = 500, message = "上传失败", imgName = "" };
                    }
                }
            }
            catch
            {
                result.Data = new { statusCode = 500, message = "上传失败", imgName = "" };
            }

            return result;
        }
    }
}