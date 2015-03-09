<?php

//     Download AWS SDK for PHP
//     AWS Console: account > security credentials > generate new access key for key and secret


require 'aws-autoloader.php';
use Aws\Ec2\Ec2Client;
set_time_limit(300);

$ec2Client = Ec2Client::factory(array(
    'key'        => 'AKIAJXEQJRDHC7T7HE6A',
    'secret'    => '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 ',
    'region'    => 'us-west-2'
));

// Launch an instance with the key pair and security group
$result = $ec2Client->runInstances(array(
    'ImageId'        => 'ami-5df2ab6d',
    'MinCount'        => 1,
    'MaxCount'        => 1,
    'InstanceType'    => 't1.micro',
    'KeyName'        => 'info344',
    'SecurityGroups'=> array('default'),
));

$instanceIds = $result->getPath('Instances/*/InstanceId');

// Wait until the instance is launched
$ec2Client->waitUntilInstanceRunning(array(
    'InstanceIds' => $instanceIds,
));

// Describe the now-running instance to get the public URL
$result = $ec2Client->describeInstances(array(
    'InstanceIds' => $instanceIds,
));

echo current($result->getPath('Reservations/*/Instances/*/PublicDnsName'));

?>