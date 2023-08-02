const paymentForm = document.getElementById('paymentForm');
paymentForm.addEventListener("submit", payWithPaystack, false);
function payWithPaystack(e) {
  e.preventDefault();

  let handler = PaystackPop.setup({
    key: 'pk_test_df4b0fd403b05bb47524d2f1156f5c94f31e2063', // Replace with your public key
    email: document.getElementById("email-address").value,
    amount: document.getElementById("amount").value * 100,
    ref: ''+Math.floor((Math.random() * 10000) + 1),  // generates a pseudo-unique reference. Please replace with a reference you generated. Or remove the line entirely so our API will generate one for you
    // label: "Optional string that replaces customer email"
    onClose: function(){
      alert('Window closed.');
    },
    callback: function(response){
      let message = 'Payment complete! Reference: ' + response.reference;
      alert(message);
         // if(document.getElementById("amount").value == "200")
         // {
            //var x = document.getElementById("email-address").value;
            // window.location = `https://localhost:7119/RDMS/UpdateToHasPaid?userEmail=${x}`;
          //}
          //else{

             //window.location = 'https://localhost:7119/RequestedProduct/UpdateToHasPaid';
         // }
    }
    
  });
  handler.openIframe();
}



  //   <script>
  //   // Create a Paystack instance with your public key
  //   var email1 = document.querySelector("#transactionEmail").value;
  //   var price = document.querySelector("#transactionPrice").value;
  //   var generate = Math.floor(Math.random() * 1000000);

  //   const paystack = window.PaystackPop.setup({
  //     key: 'sk_test_cc9e7c520919c2304bc20ff58000a284ea738771',
  //     email: email1,
  //     amount: price, // Amount in kobo (e.g., 10000 for ₦100.00)
  //     currency: 'NGN', // Currency code
  //     ref: 'Ref/' + generate, // Unique reference for the transaction

  //     onClose: function() {
  //       console.log('Payment window closed');
  //     },
  //     callback: function(response) {
  //       console.log('Payment completed. Reference: ' + response.reference);
  //       // Perform any necessary actions after successful payment
  //       // e.g., send payment details to your server for verification
  //     }
  //   });

  //   // Function to initiate the payment
  //   function initiatePayment() {
  //     paystack.openIframe();
  //   }

  //   // Call the initiatePayment function when the button is clicked
  //   document.getElementById('payButton').addEventListener('click', initiatePayment);
  // </script>

  