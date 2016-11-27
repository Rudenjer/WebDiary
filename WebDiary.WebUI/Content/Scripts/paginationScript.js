$(".paginationButton")
        .click(function () {
            var id = $(this).text();
            var pageNumber = $(".pageNumber");
            pageNumber.val(id);
        });