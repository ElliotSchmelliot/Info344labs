<html>
<head>
    <title></title>
</head>
<body>
    <form action="getEvenNumbers.php" method="GET">
        <input id="num-input" name="n" type="text"></input>
        <input type="submit" value="Submit">
    </form>
    <?php
        if (isset($_GET['n'])) {
            # print evens up to n
            $n = $_GET['n'];
            echo "n = $n<br>";
            echo "evens: ";
            for ($i = 2; $i <= $n; $i += 2) {
                echo $i . ' ';
            }

            # print primes up to n
            echo "<br> primes: ";
            for ($j = 1; $j <= $n; $j++) {
                $divis = 0;
                for ($c = 1; $c <= $j; $c++) {
                    if ($j % $c == 0) {
                        $divis++;
                    }
                }
                if ($divis < 3) {
                    echo $j . ' ';
                }
            }
        }
    ?>
</body>
</html>