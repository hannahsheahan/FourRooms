<?php 

// This php script recieves posted data from the Unity WebGL build and sends it to a JSON file on the server
// Author: Hannah Sheahan, sheahan.hannah@gmail.com
// Date: 17/11/2018



$data = $_POST["gameData"];
$fileName = $_POST["fileName"];


$serverDataPath = "http://185.47.61.11/sandbox/tasks/hannahs/martinitask/pilot/data/";
$filePath = $serverDataPath . $fileName;

//$filePath = $fileName;   // for writing to local file


// Note: if we have problems with writing permissions, create folder by filename to store file in

 // create new directory with 744 permissions if it does not exist yet
 // owner will be the user/group the PHP script is run under (watch out for this 
 // - could mean that only player gets access to the data... ***HRS to test)
$dir = $serverDataPath . 'myDir';
if ( !file_exists($dir) ) {
    mkdir ($dir, 0744);
}
$filePath = $dir . "/" . $fileName;

 
if ($data != "")
{
    echo("Trying to write data to server at: " . $filePath);
    $file = fopen($filePath, "w");  // overwrite this file with each update
    fwrite($file, $data);
    
    if (is_writable($filePath))
    {
        echo("\n The datapath location on server is writable.");
    }
    else
    {
        echo("\n The datapath location on server is not writable. Please check permissions.");
    }
    
    fclose($file);
} else
{
    echo("Message delivery failed...");
}

?>