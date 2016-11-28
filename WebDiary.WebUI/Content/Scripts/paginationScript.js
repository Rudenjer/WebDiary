$(".paginationButton")
        .click(function () {
            alert("Hello");
            var id = $(this).text();
            var pageNumber = $(".pageNumber");
            pageNumber.val(id);
        });