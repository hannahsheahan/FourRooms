<?php 

// This php script recieves posted data from the Unity WebGL build and sends it to a JSON file on the server
// Author: Hannah Sheahan, sheahan.hannah@gmail.com
// Date: 17/11/2018

$data = $_POST["gameData"];
$fileName = $_POST["fileName"];

$serverDataPath = "http://185.47.61.11/sandbox/tasks/hannahs/martinitask/pilot/data/";
$filePath = $serverDataPath . $fileName;

if ($data != "")
{
    echo("Message successfully sent!");
    $file = fopen($filePath, "w");  // overwrite this file with each update
    fwrite($file, $data);
    fclose($file);
} else
{
    echo("Message delivery failed...");
}

?>