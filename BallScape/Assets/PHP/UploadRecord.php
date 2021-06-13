<?php

header("Access-Control-Allow-Origin: *");

$conn = mysqli_connect("localhost", "id17037195_ballscapeinfo", "pJsbv}mA&847l?v1", "id17037195_ballscape");

$loginUser = $_POST["username"];
$killAmmount = $_POST["killAmmount"];

if (!$conn)
{
    echo "Error: Not connected...";
}
else
{
    $sql = "INSERT INTO Leaderboard (Username, Kills) VALUES ('" . $loginUser . "','" . $killAmmount . "')";

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