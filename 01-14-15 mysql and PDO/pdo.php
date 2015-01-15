<html>
<head>
    <title></title>
</head>
<body>
    <?php
        $username = 'info344mysqlpdo';
        $password = 'chrispaul';
        $price = 5; #user-supplied data
        try {
            $conn = new PDO('mysql:host=uwinfo344.chunkaiw.com;dbname=info344mysqlpdo', $username, $password);
            $conn->setAttribute(PDO::ATTR_ERRMODE, PDO::ERRMODE_EXCEPTION);

            $data = $conn->query('SELECT * FROM Books WHERE price > ' . $conn->quote($price));
            foreach($data as $row) {
                print_r($row);
            }
        } catch(PDOException $e) {
            echo 'ERROR: ' . $e->getMessage();
        }
    ?>
</body>
</html>