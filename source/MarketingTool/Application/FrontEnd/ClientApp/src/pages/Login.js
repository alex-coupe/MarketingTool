import React from 'react';
import Container from '../components/layout/Container';
import Button from '../components/forms/Button';
import EmailInput from '../components/forms/EmailInput';
import PasswordInput from '../components/forms/PasswordInput';
import { Card, CardBody } from '../components/layout/Card';
import { Link } from 'react-router-dom';
import { GridContainer, GridItem } from '../components/layout/Grid';

export default function Login() {

    return (
        <Container className="text-center">
            
            <Card>
                <CardBody>
                    <GridContainer>
                        <GridItem>
                            <EmailInput labelText="Email Address" name="EmailAddress" />
                        </GridItem>
                        <GridItem>
                            <PasswordInput labelText="Password" name="Password" />
                        </GridItem>
                        <GridItem>
                            <Button type="Submit" className="btn btn-primary">Login</Button>
                        </GridItem>
                    </GridContainer>
                </CardBody>
            </Card>
            <p>Not Signed up? Click <Link to="/register">Here</Link> to register</p>
        </Container>
    );

}