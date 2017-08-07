<!DOCTYPE html>
<html>
  <head>
    <meta charset='UTF-8'>
    <title>Template</title>
  </head>
  <body>
    <h2>Raw File Output</h2>
    <?php
    include('Car.php');
    $detail = file("Top50Cars.csv");
    foreach($detail as $car)
      $cars[] = explode(",",$car);
    $cars[0] = null;
    foreach($detail as $value)
      echo "<p> $value </p>";
    foreach($cars as $piece){
      if($piece != null){
      $c = new Car();
      $c->setRank($piece[0]);
      $c->setYear($piece[1]);
      $c->setMake($piece[2]);
      $c->setModel($piece[3]);
      $carItems[] = $c;
      }
    }
    echo "<h2>Table of Cars</h2><table border ='1'>";
      echo "<tr>";
      echo "<td>Rank</td>";
      echo "<td>Year</td>";
      echo "<td>Make</td>";
      echo "<td>Model</td>";      
      echo "</tr>";
    foreach($carItems as $line){
      echo "<tr>";
      echo "<td>".$line->getRank()."</td>";
      echo "<td>{$line->getYear()}</td>";
      echo "<td>{$line->getMake()}</td>";
      echo "<td>{$line->getModel()}</td>";      
      echo "</tr>";
    }
    foreach ($carItems as $key => $car)
      $make[$key] = $car->getMake();
    array_multisort($make, SORT_ASC, $carItems);
    echo "</table><h2>Table of Cars Sorted By Make</h2><table border ='1'>";
      echo "<tr>";
      echo "<td>Rank</td>";
      echo "<td>Year</td>";
      echo "<td>Make</td>";
      echo "<td>Model</td>";      
      echo "</tr>";
      foreach($carItems as $line){
      echo "<tr>";
      echo "<td>{$line->getRank()}</td>";
      echo "<td>{$line->getYear()}</td>";
      echo "<td>{$line->getMake()}</td>";
      echo "<td>{$line->getModel()}</td>";      
      echo "</tr>";
   }
    ?>
    </table>
  </body>
</html>