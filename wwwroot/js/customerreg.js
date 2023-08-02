function validateRegistration(event) {
    event.preventDefault();
  
    var firstname = document.getElementById("FirstName").value;
    var lastname = document.getElementById("LastName").value;
    var email = document.getElementById("Email").value;
    var phonenumber = document.getElementById("PhoneNumber").value;
    var password = document.getElementById("Password").value;
    var street = document.getElementById("Street").value;
    var city = document.getElementById("City").value;
    var state = document.getElementById("State").value;
    var zipCode = document.getElementById("ZipCode").value;
    var errorElement = document.getElementById("registrationError");
  
    errorElement.innerHTML = ""; // Clear any previous error message

    if (firstname === "" || lastname === "" || email === "" || phonenumber === "" || password === "" || street === ""  || city === ""  || state === ""  || zipCode === "" )
     {
        errorElement.innerHTML = "Please fill in all fields.";
        return;
      }
      

  
    // if (firstname === "") {
    //   errorElement.innerHTML = "FirstName is required.";
    //   return;
    // }
  
    // if (lastname === "") {
    //   errorElement.innerHTML = "LastName is required.";
    //   return;
    // }
  
    // if (email === "") {
    //   errorElement.innerHTML = "email is required.";
    //   return;
    // }

    // if (phonenumber === "") {
    //   errorElement.innerHTML = "phonenumber is required.";
    //   return;
    // }
    // if (street === "") {
    //   errorElement.innerHTML = "street addres is required.";
    //   return;
    // }
  
    // if (password !== confirmPassword) {
    //   errorElement.innerHTML = "Passwords do not match.";
    //   return;
    // }
  
    // if (email === "") {
    //   errorElement.innerHTML = "Email is required.";
    //   return;
    // }
  
    // Additional validation logic if needed
  
    // Registration successful, you can redirect or perform other actions here
    alert("Registration successful!");
  }
  
 
  