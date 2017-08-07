<?php
	$a = "-2 ducks"; 
	$k = "3 ducks";
	$b = "a minion"; 
	$c = false;
	$d = NULL; 
	$e = -128.34; 
	$f = 0.0;
	$g = ""; 
	$h = 42; 
	$i = -12;
	$j = array ("300", "the Avengers", "Andy", "Emma");
    
  function createRow($var){
    ?>
    <td style='text-align:center'> <?php echo $var; ?> </td>
    <td style='text-align:center'> <?php echo gettype($var); ?> </td> 
    <td style='text-align:center'> <?php echo (bool)$var; ?> </td> 
    <td style='text-align:center'> <?php echo (int)$var; ?> </td>
    <td style='text-align:center'> <?php echo (float)$var; ?> </td>
    <td style='text-align:center'> <?php echo $var; ?> </td>
    <td style='text-align:center'> <?php echo var_dump($var); ?> </td>
    <td style='text-align:center'> <?php echo is_int($var)? dechex($var):""; ?> </td>
    </tr>
  <?php
  }
?>

<!DOCTYPE html>
<html>
  <head>
    <meta charset='UTF-8'>
    <title>Variable Test</title>
  </head>
  <body>
    <table border='1'> 
  <thead>
    <tr>
      <th style='text-align:center'>Initial Value</th> 
      <th style='text-align:center'>Initial Type</th> 
      <th style='text-align:center'>Cast to bool</th> 
      <th style='text-align:center'>Cast to int</th> 
      <th style='text-align:center'>Cast to float</th>
      <th style='text-align:center'>Cast to string</th> 
      <th style='text-align:center'>Variable info</th> 
      <th style='text-align:center'>Int as hex</th>
    </tr>
   <?php
    createRow($a);
    createRow($b);
    createRow($c);
    createRow($d);
    createRow($e);
    createRow($f);
    createRow($g);
    createRow($h);
    createRow($i);
    createRow($j);
    ?>
  </thead>
    </table>
  </body>
</html>