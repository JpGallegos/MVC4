
Bootstrap_alert = function () { }

/**
    Bootstrap Alerts -
    Function Name - showAlert()
    Inputs - message, alerttype, targetId,
    Examples - Bootstrap_alert.showAlert("Invalid login", "alert-error", "#alert-container", 3000)
    Types of alerts - "alert-success", "alert-info", "alert-warning", "alert-danger" 
    Required - Bootstrap.css, Bootstrap.js
**/
Bootstrap_alert.showAlert = function (message, alerttype, targetId, timeout) {   
    $(targetId).html('<div id="bootstrap-alert" class="alert ' + alerttype + ' alert-dismissable fade in out"><button type="button" class="close" data-dismiss="alert" aria-hidden="true">&times;</button><span>' + message + '</span></div>');
               
    $("#bootstrap-alert").fadeOut(timeout);
}

