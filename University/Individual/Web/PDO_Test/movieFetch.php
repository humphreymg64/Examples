<?php
	$error = "";
  if (isset($_GET['minimumYear']) && $_GET['minimumYear'] < 2017 && $_GET['minimumYear'] > 1934 && is_numeric($_GET['minimumYear'])){
    $min = (int)$_GET['minimumYear'];
  }
  else{
    $error = $error."<h1>Error in minimum year.</h1><br>";
    $min = 1935;
  }

  if (isset($_GET['maximumYear']) && $_GET['maximumYear'] >= $min && $_GET['maximumYear'] > 1934 && $_GET['maximumYear'] < 2017 && is_numeric($_GET['maximumYear'])){
    $max = (int)$_GET['maximumYear'];
  }
  else{
    $error = $error."<h1>Error in maximum year.</h1><br>";
    $max = 2016;
  }

  if (isset($_GET['orderBy']) && ($_GET['orderBy'] == 'title' || $_GET['orderBy'] == 'studio' ||$_GET['orderBy'] == 'rating'
      ||$_GET['orderBy'] == 'pub_year' ||$_GET['orderBy'] == 'imdb_rating' ||$_GET['orderBy'] == 'run_time')){
    $order = $_GET['orderBy'];
  }
  else{
    $error = $error."<h1>Error in maximum order by.</h1><br>";
    $order = 'title';
  }

  if (isset($_GET['returnLimit']) && ($_GET['returnLimit'] == 1 || $_GET['returnLimit'] == 5 ||$_GET['returnLimit'] == 10
      ||$_GET['returnLimit'] == 25 ||$_GET['returnLimit'] == 50 ||$_GET['returnLimit'] == 75 ||$_GET['returnLimit'] == 100)){
    $count = $_GET['returnLimit'];
  }
  else{
    $error = $error."<h1>Error in count.</h1><br>";
    $count = 100;
  }
  if ($error == ""){
		$db = new PDO("mysql:host=localhost;dbname=mcmeen", "mcmeen", "12345");
		$myQuery = "select * from movie where pub_year >= :minYear AND pub_year <= :maxYear order by $order desc limit $count";
		$results = $db->prepare($myQuery);
  	$results->execute(array(
  	  'minYear'=>$min,
  	  'maxYear'=>$max));
	}
	else{
		echo $error;
	}
?>

<!DOCTYPE html>
<html>
	<head>
		<meta charset='utf-8' />
		<title>Database Request to Movie Table</title>
	</head>
	<body>
		<?php	if($error == ""){ ?>
    <table style="border:2px solid;" align="center">
      <tr style="border:2px solid;" align="center">
        <th style="border:2px solid;" align='center'>Title</th>
        <th style="border:2px solid;" align='center'>Studio</th>
        <th style="border:2px solid;" align='center'>MPAA Rating</th>
        <th style="border:2px solid;" align='center'>Publication Year</th>
        <th style="border:2px solid;" align='center'>IMDB Rating</th>
        <th style="border:2px solid;" align='center'>Run Time</th>
      </tr>
      <?php
				}
        while($error == "" && $row = $results->fetch() ) 
        {
          echo "<tr style='border:2px solid;' align='center'><td style='border:2px solid;'>".$row['title']."</td>"; 
          echo "<td style='border:2px solid;' align='center'>".$row['studio']."</td>"; 
          echo "<td style='border:2px solid;' align='center'>".$row['rating']."</td>"; 
          echo "<td style='border:2px solid;' align='center'>".$row['pub_year']."</td>"; 
          echo "<td style='border:2px solid;' align='center'>".$row['imdb_rating']."</td>"; 
          echo "<td style='border:2px solid;' align='center'>".$row['run_time']."</td></tr>"; 
        }
      ?>
    </table>
    <a href='http://einstein.etsu.edu/~humphreymg/PDO_Test/movieForm.php' align="center">Return to the previous page.</a>
	</body>
</html>