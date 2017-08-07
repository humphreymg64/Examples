<!DOCTYPE html>
<html>
  <head>
    <meta charset='UTF-8'>
    <title>Template</title>
  </head>
  <body>
    <h2>Raw File Output</h2>
    <?php
    $detail = file("Top50Cars.csv");
    foreach($detail as $car)
      $cars[] = explode(",",$car);
    foreach($detail as $key => $value)
      echo "<p> $value </p>";
    echo "<h2>Table of Cars</h2><table border ='1'>";
    foreach($cars as $line){
      echo "<tr>";
      foreach($line as $v)
        echo "<td> $v </td>";
      echo "</tr>";
    }
    $temp = $cars[0];
    $cars[0] = null;
    foreach ($cars as $key => $car)
      $make[$key] = $car[2];
    array_multisort($make, SORT_ASC, $cars);
    $cars[0] = $temp;
    echo "</table><h2>Table of Cars Sorted By Make</h2><table border ='1'>";
      foreach($cars as $line){
        echo "<tr>";
        foreach($line as $v)
          echo "<td> $v </td>";
        echo "</tr>";
     }
     ?>
    </table>
  </body>
</html>