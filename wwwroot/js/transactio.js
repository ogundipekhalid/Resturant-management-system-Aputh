// Create a Paystack instance with your public key
var email1 = document.querySelector("#transactionEmail")
var form = document.querySelector("#transaction")
var price = document.querySelector("#transactionPrice")
var generate =  Math.floor(Math.random() * 1000000);

const paystack = window.PaystackPop.setup({
    key: 'sk_test_cc9e7c520919c2304bc20ff58000a284ea738771',
    email: email1,
    amount: price, // Amount in kobo (e.g., 10000 for ₦100.00)
    currency: 'NGN', // Currency code
     ref: 'Ref/(generate)', // Unique reference for the transaction

    onClose: function() {
      console.log('Payment window closed');
    },
    callback: function(response) {
      console.log('Payment completed. Reference: ' + response.reference);
      // Perform any necessary actions after successful payment
      // e.g., send payment details to your server for verification
    }
  });
  
  // Function to initiate the payment
  function initiatePayment() {
    paystack.openIframe();
  }
  
  // Call the initiatePayment function when a button is clicked or any other event occurs
  document.getElementById('payButton').addEventListener('click', initiatePayment);
  


  

