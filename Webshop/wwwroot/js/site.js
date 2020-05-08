// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

const ADDTOCART_URL = 'https://localhost:44318/Cart/Add?productId='
function AddToCart(productid) {
    fetch(ADDTOCART_URL + productid)
        .then(function(res) {
            console.log(res);
        })
};