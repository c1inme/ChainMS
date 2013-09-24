$(document).ready(function () {
   
    var inputTag = ".upload";
    var imgTag = "#imgThumbnail2";
   // $(imgTag).hide();
    $(inputTag).change(function () {
        //$.noConflict(true);
        //-- Create a input attribute with the same type as the original
        //-- Insert it after the orignals location.
       // $(inputTag).appendTo("<input  class='upload-clone' type='file' name='name-clone'/>");

        $("<input>").attr(
            {
                'class':"upload-clone",
                type: $(inputTag).attr("type"),
                name: "name-clone"

            }).insertAfter(inputTag);
        //var d = new Date();
        //var guid = d.getMilliseconds() + d.getTime();
        //--Create a hidden form with an action method pointer to
        //--our custom controller. 



        $("<form>").attr(
			{
			    method: "post",
			    id: "prototype",
			    action: "/ImageHelper/AjaxSubmit/1" 

			}).appendTo("body").hide();

        //--Change the encoding based on the browser, as IE doent allow you to change the encoding.
        //--Append our orignal input to the hidden form.
        $("#prototype").attr((this.encoding ? "encoding" : "enctype"), "multipart/form-data");
        $(imgTag)[0].src = "";
        $(inputTag).appendTo("#prototype").hide();
       
        //--Use AJAX to post the form, and if successful, load the binary info the image tag.
        $("#prototype").ajaxSubmit(
                {
                    success: function (responseText) {
                        var d = new Date();
                       
                        if (responseText != "") {
                            $(imgTag).show();
                            $(imgTag)[0].src = "/ImageHelper/ImageLoad/1";
                        }
                        else {
                            $(imgTag).hide();
                        }

                        $(inputTag).insertAfter(imgTag).show();
                        $(".upload-clone").remove();
                        $('#prototype').remove();
                    }
                });
        /*
        $('upload-clone').appendTo('#prototype');
        $('#prototype').remove();
        */

    });
});

