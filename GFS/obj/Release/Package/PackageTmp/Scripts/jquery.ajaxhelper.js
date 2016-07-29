// For documentation about JQuery's AJAX, check this link: https://api.jquery.com/jQuery.ajax/

function AjaxPOST(url, data, dataType, beforeSendFunction, successFunction, errorFunction, completeFunction) {

    /// <summary>Executes a request that POST data to the given url.</summary>
    /// <param name="url" type="String">The URL to post data.</param>
    /// <param name="data" type="MIME type">The data to post. Example: A form type of FormData().</param>
    /// <param name="dataType" type="String">The type of the data to post. Can be xml, html, script, json, jsonp and text.</param>
    /// <param name="beforeSendFunction" type="Function">Function to be executed before the POST.</param>
    /// <param name="successFunction" type="Function"> Function to be executed after the POST got succeeded. Returns data, textStatus and jqXHR.</param>
    /// <param name="errorFunction" type="Function">Function to be executed when the POST fails. Returns jqXHR, textStatus and errorThrown.</param>
    /// <param name="completeFunction" type="Function">Function to will always be executed after the whole call. Returns data, textStatus and errorThrown.</param>
    /// <returns type="XMLHttpRequest">The request.</returns>

    // Translates the method to the general request method.
    var request = GeneralAjax(url, 'POST', beforeSendFunction, successFunction, errorFunction, completeFunction, data, dataType);

    return request;
}
function AjaxGET(url, dataType, beforeSendFunction, successFunction, errorFunction, completeFunction) {

    /// <summary>Executes a request that GET data from the given url.</summary>
    /// <param name="url" type="String">The URL to post data.</param>
    /// <param name="dataType" type="String">The type of the data to post. Can be xml, html, script, json, jsonp and text.</param>
    /// <param name="beforeSendFunction" type="Function">Function to be executed before the POST.</param>
    /// <param name="successFunction" type="Function"> Function to be executed after the POST got succeeded. Returns data, textStatus and jqXHR.</param>
    /// <param name="errorFunction" type="Function">Function to be executed when the POST fails. Returns jqXHR, textStatus and errorThrown.</param>
    /// <param name="completeFunction" type="Function">Function to will always be executed after the whole call. Returns data, textStatus and errorThrown.</param>
    /// <returns type="XMLHttpRequest">The request.</returns>

    // Translates the method to the general request method.
    var request = GeneralAjax(url, 'GET', beforeSendFunction, successFunction, errorFunction, completeFunction, null, dataType);

    return request;
}

function GeneralAjax(url, method, beforeSendFunction, successFunction, errorFunction, completeFunction, postData, dataType) {

    /// <summary>A general function to execute AJAX requests.</summary>
    /// <param name="url" type="String">The URL to post data.</param>
    /// <param name="method" type="String">The type of request to make. Can be 'GET' or 'SET'.</param>
    /// <param name="beforeSendFunction" type="Function">Function to be executed before the POST.</param>
    /// <param name="successFunction" type="Function"> Function to be executed after the POST got succeeded. Returns data, textStatus and jqXHR.</param>
    /// <param name="errorFunction" type="Function">Function to be executed when the POST fails. Returns jqXHR, textStatus and errorThrown.</param>
    /// <param name="completeFunction" type="Function">Function to will always be executed after the whole call. Returns data, textStatus and errorThrown.</param>
    /// <param name="data" type="MIME type">The data to post. Example: A form type of FormData().</param>
    /// <param name="dataType" type="String">The type of the data to post. Can be xml, html, script, json, jsonp and text.</param>
    /// <returns type="XMLHttpRequest">The request.</returns>

    // Check and execute the beforeSend function.
    if (beforeSendFunction != null) {
        beforeSendFunction();
    }

    // Check and execute the request according to the given request method.
    if (method == 'GET') {
        var request = $.ajax({
            url: url,
            type: 'GET',
            dataType: dataType,
            processData: false
        });
    }
    else if (method == 'POST') {
        var request = $.ajax({
            url: url,
            type: 'POST',
            data: postData,
            dataType: dataType,
            cache: false,
            contentType: false,
            processData: false
        });
    }

    // Check and execute the successFunction function.
    if (successFunction != null) {
        request.done(function (data, textStatus, jqXHR) { successFunction(data, textStatus, jqXHR) });
    }

    // Check and execute the errorFunction function.
    if (errorFunction != null) {
        request.fail(function (jqXHR, textStatus, errorThrown) { errorFunction(jqXHR, textStatus, errorThrown) });
    }

    // Check and execute the completeFunction function.
    if (completeFunction != null) {
        request.always(function (data, textStatus, errorThrown) { completeFunction(data, textStatus, errorThrown) });
    }

    return request;
}