'use strict';
const datepicker = document.getElementsByClassName('datePicker');
const dateTimepicker = document.getElementsByClassName('dateTimepicker');
const timePicker = document.getElementsByClassName('timePicker');

let date = new Date();
let fp = flatpickr(datepicker,
    {
        enableTime: false,
        dateFormat: "d/m/Y",
        time_24hr: false,
    });
fp = flatpickr(timePicker,
    {
        enableTime: true,
        noCalendar: true,
        time_24hr: false,
        dateFormat: "h:i:K",
    });

fp = flatpickr(dateTimepicker,
    {
        enableTime: true,
        dateFormat: "d/m/Y h:i:K",
        time_24hr: false,
        minDate: "today",
        maxDate: new Date().fp_incr(1) // 14 days from now
    });





