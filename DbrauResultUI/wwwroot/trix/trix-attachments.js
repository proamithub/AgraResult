(function () {
    /*var HOST = "https://d13txem1unpe48.cloudfront.net/"*/
    /*var HOST = "https://dotnettrickscloud.blob.core.windows.net/"*/
   
    Trix.config.attachments.preview.caption = {
        name: false,
        size: false,
        attachmentCaption: false,

    };
    //Trix.config.blockAttributes.default.tagName = "p"
    //Trix.config.blockAttributes.default.breakOnReturn = true
    //Trix.config.textAttributes.underline = {
    //    style: {
    //        textDecoration: "underline",
    //    },
    //    inheritable: true,
    //    parser: (element) => {
    //        const style = window.getComputedStyle(element);

    //        return style.textDecoration === "underline" ||
    //            style.textDecorationLine === "underline";
    //    }
    //}    
    Trix.config.blockAttributes.paragraph = {
        tagName: 'p',
        terminal: true,
        breakOnReturn: true,
        group: false
    }
    Trix.config.blockAttributes.heading2 = {
        tagName: 'h2',
        terminal: true,
        breakOnReturn: true,
        group: false
    }
    Trix.config.blockAttributes.heading3 = {
        tagName: 'h3',
        terminal: true,
        breakOnReturn: true,
        group: false
    }
    Trix.config.blockAttributes.heading4 = {
        tagName: 'h4',
        terminal: true,
        breakOnReturn: true,
        group: false
    }
    Trix.config.blockAttributes.heading5 = {
        tagName: 'h5',
        terminal: true,
        breakOnReturn: true,
        group: false
    }
    Trix.config.blockAttributes.heading6 = {
        tagName: 'h6',
        terminal: true,
        breakOnReturn: true,
        group: false
    }
    Trix.config.blockAttributes.table = {
        tagName: 'table',
        terminal: true,
        breakOnReturn: true,
        group: false
    }
    Trix.config.blockAttributes.table = {
        tagName: 'table',
        terminal: true,
        breakOnReturn: true,
        group: false
    }
    Trix.config.textAttributes.SourceCode = {
        tagName: 'SourceCode'
    }
    
    addEventListener("trix-initialize", event => {
        const { toolbarElement } = event.target
        const h1Button = toolbarElement.querySelector("[data-trix-attribute=heading1]")
        h1Button.insertAdjacentHTML("afterend", `
    <button type="button" class="trix-button" data-trix-attribute="heading2" title="Heading 2" tabindex="-1" data-trix-active="">H2</button>
  `)
        const h2Button = toolbarElement.querySelector("[data-trix-attribute=heading2]")
        h2Button.insertAdjacentHTML("afterend", `
    <button type="button" class="trix-button" data-trix-attribute="heading3" title="Heading 3" tabindex="1" data-trix-active="">H3</button>
  `)
        const h3Button = toolbarElement.querySelector("[data-trix-attribute=heading3]")
        h3Button.insertAdjacentHTML("afterend", `
    <button type="button" class="trix-button" data-trix-attribute="heading4" title="Heading 4" tabindex="1" data-trix-active="">H4</button>
  `)
        const h4Button = toolbarElement.querySelector("[data-trix-attribute=heading4]")
        h4Button.insertAdjacentHTML("afterend", `
    <button type="button" class="trix-button" data-trix-attribute="heading5" title="Heading 5" tabindex="1" data-trix-active="">H5</button>
  `)
        const h5Button = toolbarElement.querySelector("[data-trix-attribute=heading5]")
        h5Button.insertAdjacentHTML("afterend", `
    <button type="button" class="trix-button" data-trix-attribute="heading6" title="Heading 6" tabindex="1" data-trix-active="">H6</button>
  `)
        const pButton = toolbarElement.querySelector("[data-trix-attribute=heading6]")
        pButton.insertAdjacentHTML("afterend", `
    <button type="button" class="trix-button" data-trix-attribute="paragraph" title="Paragraph" tabindex="1" data-trix-active="">P</button>
  `)
        const tButton = toolbarElement.querySelector("[data-trix-attribute=paragraph]")
        tButton.insertAdjacentHTML("afterend", `
    <button type="button" class="trix-button" data-trix-attribute="table" title="table" tabindex="1" data-trix-active=""><i class="fas fa-table"></i></button>
  `)
        const scButton = toolbarElement.querySelector("[data-trix-button-group=file-tools]")
        scButton.insertAdjacentHTML("afterend", `
    <label class="switch" data-trix-attribute="sourceCode" title="SourceCode" data-trix-action="SourceCode"><input type="checkbox" checked><span class="slider round"></span></label>
  `)
    })
    addEventListener("click", function (event) {
        //debugger;
        var element = document.querySelector("trix-editor")
        element.editor
        range = element.editor.getSelectedRange()
        var selectedText = element.editor.getDocument().getStringAtRange(range) 
        if (event.target.className == "fas fa-table") {
            element.editor.insertHTML("<Table><tr><td>cell</td><td>cell</td></tr></table>");
        }       
    })

    function SourceCode() {
        alert("hi");
    }
    addEventListener("trix-attachment-add", function (event) {
        if (event.attachment.file) {
            uploadFileAttachment(event.attachment)
        }
        
    })

    function uploadFileAttachment(attachment) {
        uploadFile(attachment.file, setProgress, setAttributes)

        function setProgress(progress) {
            attachment.setUploadProgress(progress)
        }

        function setAttributes(attributes) {
            attachment.setAttributes(attributes)
        }
    }

    function uploadFile(file, progressCallback, successCallback) {
        var formData = createFormData(file)
        var url = "/Admin/Tutorial/UploadFileDetail"
        var xhr = new XMLHttpRequest()
        xhr.open("POST", url, true)

        xhr.upload.addEventListener("progress", function (event) {
            var progress = event.loaded / event.total * 100
            progressCallback(progress)
        })

        xhr.addEventListener("load", function (event) {
            /*if (xhr.status == 204) {*/
            if (xhr.status == 200) {
                var HOST = $("#hidImgCloudPath").val();
                var attributes = {                    
                    url: HOST + xhr.responseText,
                    href: HOST + xhr.responseText + "?content-disposition=attachment"
                }
                successCallback(attributes)
            }
        })

        xhr.send(formData)
    }

    function createFormData(file) {
        var data = new FormData()
        data.append("Content-Type", file.type)
        data.append("file", file)
        return data
    }  

    addEventListener("trix-attachment-remove", function (event) {
        //let text = "Are you sure, you want to delete this image.";
        //if (confirm(text) == true) {
            if (event.attachment) {
                // debugger;
                removeuploadFileAttachment(event.attachment)
            }
        //} else {
        //    event.preventDefault(); 
        //}
       
    })
    function removeuploadFileAttachment(attachment) {
        var filename = attachment.attachment.previewURL.substring(attachment.attachment.previewURL.lastIndexOf('/') + 1)
        removeFile(filename)
    }
    function removeFile(file) {
        var filename = file
        var url = "/Admin/Tutorial/deleteFile"
        $.ajax({
            url: url,
            type: "POST",
            contentType: "application/json; charset=utf-8", // Not to set any content header  
            processData: false, // Not to process data  
            data: JSON.stringify(filename),
            success: function (result) {
                //alert(result);
            },
            error: function (err) {
                //alert(err.statusText);
            }
        });  
    }

})();
