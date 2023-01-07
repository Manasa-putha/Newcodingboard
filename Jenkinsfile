pipeline {
  agent any
  environment {
    dotnet = 'C:\Program Files\dotnet\dotnet.exe'
  }
  stages {
    stage('Checkout Stage') {
      steps {
        
        git  url:'https://github.com/Manasa-putha/Newcodingboard/main/Jenkinsfile'
      }
    }
    stage('Build Stage') {
      steps {
        bat 'dotnet build'
      }
    }
    stage('Test Stage') {
      steps {
        echo "Application tested"
      }
    }
  }
}
        
