<?php

$conn = mysqli_connect("localhost", "id17037195_ballscapeinfo", "pJsbv}mA&847l?v1", "id17037195_ballscape");

if (!$conn)
{
    echo "Not connected...";
}
else
{
    $sql = "SELECT * FROM `Leaderboard` ORDER BY `Leaderboard`.`Kills` ASC LIMIT 10";

    $result = $conn->query($sql);

    if ($result->num_rows > 0)
    {
        while ($row = $result->fetch_assoc())
        {
            echo $row["Username"] . "|" . $row["Kills"] . "/";
        }
    }
    else
    {
        echo "0 results";
    }

    $conn->close();
}

?>