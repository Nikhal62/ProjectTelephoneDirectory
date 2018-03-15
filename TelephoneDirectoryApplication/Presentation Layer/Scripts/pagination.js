function PagerClick(index,firstname,lastname,address,phonetype,phonenumber)
{
    document.getElementById("hfCurrentPageIndex").value = index;
    document.forms[1].submit();
}