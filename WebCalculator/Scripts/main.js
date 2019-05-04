window.onload = function () {
    var inputElement = document.getElementById('userInput');
    var resultElement = document.getElementById('userResult');
    var historyElement = document.getElementById('calcsHistory');
    var isNewCalc = false;
    var regEx = /^[^+*\/]([\-])?([0-9]+(\,)?([0-9]+)?)?([+\-*\/])?([0-9]+(\,)?([0-9]+)?)?$/; 
    //regEx expression needs to check if user inputs are correct. 
    //It will restrict any inputs, after two operands and math operation were fully introduced.
    //In case if problems will appear https://regex101.com/r/DBuiSj/4

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
            alert('Please check if your inputs are correct.');
        }

    });

    $('.button-cancel').on('click', function () {
        resultElement.textContent = '';
        inputElement.textContent = '';
    });

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
                resultElement.textContent = 'Error appeared, check console.log() for details.'
                console.log('Bad request', data);
            }
        });
    });

    $(document).on('click', '.historical', function () {
        resultElement.textContent = '';
        inputElement.textContent = this.textContent;
    })

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