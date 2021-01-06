import React from 'react';
import Container from '../components/layout/Container';
import Button from '../components/forms/Button';
import EmailInput from '../components/forms/EmailInput';
import PasswordInput from '../components/forms/PasswordInput';
import { Card, CardBody } from '../components/layout/Card';
import { Link } from 'react-router-dom';

export default function Login() {

    return (
        <Container className="text-center">
            <Card>
                <CardBody>
                    <EmailInput labelText="Email Address" name="EmailAddress" /> 
                    <PasswordInput labelText="Password" name="Password" />
                    <Button type="Submit" className="btn btn-primary">Login</Button>
                </CardBody>
            </Card>
            <p>Not Signed up? Click <Link to="/register">Here</Link> to register</p>
        </Container>
    );

}