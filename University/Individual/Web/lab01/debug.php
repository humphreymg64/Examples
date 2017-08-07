<!DOCTYPE html> 
<html>
  <head>
    <meta charset="UTF-8">
    <title>Table of Squares and Cubes</title>
  </head>
  <body>
    <table style='width:300px'> 
      <thead>
        <tr>
          <th style='text-align:center'>n</th>
          <th style='text-align:center'>n<sup>2</sup></th> 
          <th style='text-align:center'>n<sup>3</sup></th>
        </tr>
      </thead>
      <tbody>
<?php for($i = 0; $i <= 20; $i++) { ?> 
        <tr>
          <td style='text-align:center'> 
<?php
  echo $i;
?>
          </td>
          <td style='text-align:center'> 
<?php
  echo $i * $i;
?>
          </td>
          <td style='text-align:center'>
<?php
  echo $i * $i * $i;
?>
          </td>
<?php } ?>
        </tr>
      </tbody>
    </table>
  </body>
</html>
