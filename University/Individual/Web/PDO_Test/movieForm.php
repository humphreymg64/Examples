<!DOCTYPE html>
<html>
	<head>
		<meta charset='utf-8' />
		<title>Database Request to Movie Table</title>
	</head>
	<body>
		<h1 style='text-align:center;'>Database Request to Movie Table</h1>
		<form method='get' action='movieFetch.php'>
		<table align='center' border='0' cellpadding='5'>
			<tr>
				<td colspan='2' style='text-align:center;'>
					<p>Using the form below, specify the criteria you would like to use to perform a search on the Movie database table.</p>
				</td>
			</tr>
			<tr>
				<td style='text-align:right;' width='50%'>Minimum Publication Year</td>
				<td style='text-align:left;' width='50%'>
					<select name='minimumYear'>
						<?
							// You should set the default timezone before using date() function
							date_default_timezone_set('UTC');
							for($year=1935; $year <= date("Y"); $year++)
								echo "<option value='".$year."'>".$year."</option>";
						?>
					</select>
				</td>
			</tr>
			<tr>
				<td style='text-align:right;'>Maximum Publication Year</td>
				<td style='text-align:left;'>
					<select name='maximumYear'>
						<?
							for($year=1935; $year <= date("Y"); $year++)
							{
								echo "<option value='".$year."'";
								if($year == date("Y"))
									echo " selected='selected' ";
								echo ">".$year."</option>";
							}
						?>
					</select>
				</td>
			</tr>
			<tr>
				<td style='text-align:right;'>Order by:</td>
				<td style='text-align:left;'>
					<select name='orderBy'>
						<option value='title'>Title</option>
						<option value='studio'>Studio</option>
						<option value='rating'>MPAA Rating</option>
						<option value='pub_year'>Year of Publication</option>
						<option value='imdb_rating'>IMDB Rating</option>
						<option value='run_time'>Run Time</option>
					</select>
				</td>
			</tr>
			<tr>
				<td style='text-align:right;'>Limit search to top:</td>
				<td style='text-align:left;'>
					<select name='returnLimit'>
						<option value='1'>1</option>
						<option value='5'>5</option>
						<option value='10'>10</option>
						<option value='25'>25</option>
						<option value='50'>50</option>
						<option value='75'>75</option>
						<option value='100' selected='selected'>100</option>
					</select>
				</td>
			</tr>
			<tr>
				<td style='text-align:right;'><input type='submit' value='Submit Form'></td>
				<td style='text-align:left;'><input type='reset' value='Reset Form'></td>
			</tr>
		</table>
		</form>
	</body>
</html>