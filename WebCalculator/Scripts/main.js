window.onload = function () {
    var inputElement = document.getElementById('userInput');
    var resultElement = document.getElementById('userResult');
    var historyElement = document.getElementById('calcsHistory');
    var isNewCalc = false;
    var regEx = /^[^+*\/\,]([\-\,])?([0-9]+(\,)?([0-9]+)?)?([+\-*\/])?([0-9]+(\,)?([0-9]+)?)?$/; 
    //regEx expression needs to check if user inputs are correct. 
    //It will restrict any inputs, after two operands and math operation were fully introduced.
    //In case if problems will appear https://regex101.com/r/DBuiSj/6


    $('.button').on('click', function () {
        if (isNewCalc) {
            inputElement.textContent = '';
            resultElement.textContent = '';
            isNewCalc = false;
        }

        var buttonText = this.textContent;
        var inputedText = inputElement.textContent + buttonText;

        if (regEx.test(inputedText)) {
            inputElement.textContent = inputedText;
        } else {
            alert('Пожалуйста, проверьте вводные параметры и повторите попытку.');
        }

    });

    $('.button-cancel').on('click', function () {
        resultElement.textContent = '';
        inputElement.textContent = '';
    });

    //Forming an ajax request to send user inputs to server
    $('.button-res').on('click', function () {
        $.ajax({
            type: "POST",
            url: "/Calc/Calculation",
            data: { mathExpression: inputElement.textContent },
            dataType: "json",
            success: function (response) {
                addNewHistiricalDiv(response.Time, response.MathExpression, historyElement);
                resultElement.textContent = response.Result;
                isNewCalc = true;
            },
            error: function (data) {
                resultElement.textContent = 'Ошибка. Повторите попытку.'
                alert('При расчёте данного выражения возникла ошибка: ' + inputElement.textContent + '\n Пожалуйста, проверьте правильность ввода и повторите попытку.');
                console.log('Bad request', data);
            }
        });
    });

    //Feature - to throw previous calculations to calculator by click.
    $(document).on('click', '.historical', function () {
        resultElement.textContent = '';
        inputElement.textContent = this.textContent;
    })

    //Load previous calculations, when element is ready.
    $('calc-historical-view').ready(function () {
        $.ajax({
            type: "POST",
            url: "/Calc/CalculationHistory",
            dataType: "json",
            success: function (response) {
                for (var i = 0; i < response.length; i++) {
                    addNewHistiricalDiv(response[i].Time, response[i].MathExpression, historyElement);
                }
            },
            error: function (data) {
                console.log('Bad request', data);
            }
        });
    })

    //Function to form new view element in div with historical calculations.
    function addNewHistiricalDiv(infoText, valueText, parent) {
        var infoElement = document.createElement('div');
        var valueElement = document.createElement('div');

        infoElement.appendChild(document.createTextNode(infoText))
        valueElement.style.color = '#0094ff';
        valueElement.setAttribute('class', 'historical');
        valueElement.appendChild(document.createTextNode(valueText));
        valueElement.appendChild(document.createElement('br'));
        valueElement.appendChild(document.createElement('br'));

        parent.appendChild(infoElement);
        parent.appendChild(valueElement);
    }
}