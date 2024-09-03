function store() {
    localStorage.setItem("productId",1002);
    localStorage.setItem("cartId",1);
}

async function add() {
    debugger;
    var Q = document.getElementById("inputNumber");

    const url = "https://localhost:44319/api/Cart";
    var request = {
        cartId: localStorage.getItem("cartId"),
        productId: localStorage.getItem("productId"),
        quantity: Q.value
      }
    var data = await fetch(url,
        {
            method : "POST",
            body : JSON.stringify(request),
            headers : {
                'Content-Type': 'application/json'
            }
        })
}