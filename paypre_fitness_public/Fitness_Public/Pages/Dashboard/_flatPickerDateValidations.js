const fromDate = document.getElementsByClassName('fromDatePicker');
const toDate = document.getElementsByClassName('toDatePicker');

export function bindDate() {
    flatpickr(fromDate,
        {
            enableTime: false,
            dateFormat: "d/m/Y",
            time_24hr: false,
            defaultDate: getDateOfSevenDayBefore(),
            maxDate: getDateOfSevenDayBefore(),
            onReady: function (selectedDates, dateStr, instance) {
                bindTodate(selectedDates[0]);
            },
            onChange: function (selectedDates, dateStr, instance) {
                bindTodate(selectedDates[0]);
            }
        });
}
function bindTodate(date) {

    flatpickr(toDate,
        {
            enableTime: false,
            dateFormat: "d/m/Y",
            time_24hr: false,
            defaultDate: getDateOfSevenDayAfter(date),
            minDate: getDateOfSevenDayAfter(date),
            maxDate: addDatesIntodayDate(date,28)
        });
}

function getDateOfSevenDayBefore() {
    let today = new Date();
    return new Date(today.getFullYear(), today.getMonth(), today.getDate() - 6);
}

function getDateOfSevenDayAfter(date) {
    return new Date(date.getFullYear(), date.getMonth(), date.getDate() + 7);
}

function addDatesIntodayDate(date, number) {
    return new Date(date.getFullYear(), date.getMonth(), date.getDate() + number);
}