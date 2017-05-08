function notification(title, mesage, type) {
   
    var className = 'custom-success';
    var stickyValue = true;
    if (type.toLowerCase() == "error") {
        className = 'custom-error';
        stickyValue = true;
    }
    if (type.toLowerCase() == "warning") {
        className = 'custom-warning';
        stickyValue = true;
    }
    $.gritter.add({
        // (string | mandatory) the heading of the notification
        title: title,
        // (string | mandatory) the text inside the notification
        text: mesage,
        sticky: stickyValue,//if you want to fade out it or still sit there
        class_name: className,
        //image: '/Images/clear.png',
        time: 8000 //time alive before fade out
    });
}