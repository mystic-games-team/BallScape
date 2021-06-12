<?php

$conn = mysqli_connect("localhost", "id17037195_ballscapeinfo", "pJsbv}mA&847l?v1", "id17037195_ballscape");

$loginUser = $_POST["username"];
$killAmmount = $_POST["killAmmount"];

if (!$conn)
{
    echo "Error: Not connected...";
}
else
{
    $sql = "INSERT INTO Users (Username, Kills) VALUES ('" . $loginUser . "','" . $killAmmount . "')";
    $result = $conn->query($sql);

    if ($conn->query($sql) === TRUE)
    {
    }
    else
    {
        echo "Error: " . $sql . "<br>" . $conn->error;    
    }
    

    $conn->close();
}

?>