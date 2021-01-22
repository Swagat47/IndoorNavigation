<?php
    //http://specnitharnav.epizy.com/WriteMap.php
    $servername = "sql302.epizy.com";
    $username = "epiz_27747615";
    $password = "WgcMXj2bJc6";
    $dbName = "epiz_27747615_SpecNithARNav";
    $mapName = $_POST["mapName"];

    //Make Connection
    $conn = mysqli_connect($servername,$username,$password,$dbName);

    //Check Connection
    if(!$conn){
        die("Connection Failed. " . mysqli_connect_error());
    } else {
        $sql = "SELECT info FROM map WHERE name = '$mapName'";
        $result = mysqli_query($conn,$sql);
        $row = mysqli_fetch_assoc($result);
        echo($row['info']);
    }
?>