pipeline {
  agent any
  environment {
    dotnet = 'C:\\Program Files\\dotnet\\dotnet.exe'
  }
  stages {
    stage('Checkout Stage') {
      steps {
        
        git credentialsId: '2e9e156c-ea91-4189-8e75-de3b0207816a', url:'https://github.com/Manasa-putha/Newcodingboard/blob/main/Jenkinsfile'
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
        
