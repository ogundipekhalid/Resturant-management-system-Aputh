function fetchOrders()
 {
    // Get the orders from the server
    $.ajax({
      url: "/api/orders",
      type: "GET",
      dataType: "json",
      success: function(orders) {
        // Render the orders in the DOM
        var ordersList = $("#orders-list");
        ordersList.empty();
        for (var i = 0; i < orders.length; i++) {
          var order = orders[i];
          var orderLi = $("<li>");
          orderLi.append(order.id);
          orderLi.append(order.FirstName);
          orderLi.append(order.orderDate);
          ordersList.append(orderLi);
        }
      },
      error: function(error) {
        console.log(error);
      }
    });
  }