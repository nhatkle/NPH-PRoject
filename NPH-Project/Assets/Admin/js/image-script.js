function preview_image(imageUpload, previewImage) {
    var reader = new FileReader();
    reader.onload = function (e) {
        $(previewImage).attr('src', e.target.result);
    }
    reader.readAsDataURL(event.target.files[0]);
}
/*
function ShowImagePreview(imageUpload, previewImage) {
    if (imageUploader.files && imageUploader.files[0]) {
        var reader = new FileReader();
        reader.onload = function (e) {
            $(previewImage).attr('src', e.target.result);
        }
        reader.readAsDataURL(imageUploader.files[0]);
    }
}*/
