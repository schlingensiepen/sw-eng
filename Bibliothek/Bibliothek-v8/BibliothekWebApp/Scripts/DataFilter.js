function filter() {
    // Declare variables 
    var input, filter, table, tr, td, i, ii, buf;
    input = document.getElementById("filterText");
    filter = input.value.toUpperCase();
    table = document.getElementById("filterTable");
    tr = table.getElementsByTagName("tr");

    // Loop through all table rows, and hide those who don't match the search query
    for (i = 0; i < tr.length; i++)
    {
        td = tr[i].getElementsByTagName("td");
        buf = "";
        if (td && td.length>0)
        {
            for (ii = 0; ii < td.length-1; ii++)
            {
                buf = buf + " " + td[ii].innerHTML.toUpperCase();
            }
            if (buf.indexOf(filter) > -1)
            {
                tr[i].style.display = "";
            }
            else
            {
                tr[i].style.display = "none";
            }
        }
    }
}