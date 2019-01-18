(function () {
    function init() {
        $('.add-to-cart').click(function () {
            var productId = $(this).data('id');
            addCartItem(productId);
        })
    }
    init();
    function addCartItem(productId) {
        $.post(`cart/items/${productId}`, response => {
            console.log(`Product with id: ${productId} was added to the cart.`);
        })
    }
})();



//<script>
//    toastr.options = {
//        "positionClass": "toast-top-center"
//}
//document.getElementById('updateDetails')
//                .addEventListener('click', function () {
//        toastr["success"]("fdfdfdf");
//    })
//        </script>