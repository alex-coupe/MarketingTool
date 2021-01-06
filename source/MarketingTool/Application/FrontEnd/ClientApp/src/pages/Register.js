import React from 'react';
import Container from '../components/layout/Container';
import Button from '../components/forms/Button';
import EmailInput from '../components/forms/EmailInput';
import PasswordInput from '../components/forms/PasswordInput';
import { Card, CardBody } from '../components/layout/Card';


export default function Login() {

    return (
        <Container className="text-center">
            <Card>
                <CardBody>
                 Welcome to this app! Fill in your details to register
                </CardBody>
            </Card>
           
        </Container>
    );

}