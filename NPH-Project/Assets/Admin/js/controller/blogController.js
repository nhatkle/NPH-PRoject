var user = {
    init: function () {
        user.registerEvents();
    },
    registerEvents: function () {
        $('.btn-active').off('click').on('click', function (e) {
            e.preventDefault();
            var btn = $(this);
            var id = btn.data('id');
            $.ajax({
                url: "/Admin/Blog/ChangeStatus",
                data: { id: id },
                dataType: "json",
                type: "POST",
                success: function (res) {
                    if (res.status == true) {
                        btn.text('Active');
                    }
                    else {
                        btn.text('Block');
                    }
                }

            });
        });
    }
}
user.init();