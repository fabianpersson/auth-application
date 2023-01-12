import * as React from "react";
import {
  Flex,
  Heading,
  TextField,
  PasswordField,
  Button,
  useTheme,
  Alert,
  Link,
  Card,
  Grid,
} from "@aws-amplify/ui-react";

async function registerUser(credentials) {
  return fetch("https://localhost:7271/signup", {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify(credentials),
  }).then((response) => {
    if (response.ok) {
      return true;
    } else {
      return false;
    }
  });
}

const Signup = () => {
  const { tokens } = useTheme();
  const [password, setPassword] = React.useState("");
  const [username, setUsername] = React.useState("");

  const [succeeded, setSucceeded] = React.useState();

  return (
    <Grid alignItems="center" justifyContent="center" padding="10rem">
      <Card width="20em" variation="elevated" alignItems="center">
        <Flex
          as="form"
          direction="column"
          gap={tokens.space.medium}
          justifyContent="center"
        >
          <Heading level={3}>Sign Up</Heading>
          <TextField
            label="Username"
            name="username"
            autoComplete="username"
            value={username}
            onChange={(e) => {
              setUsername(e.target.value);
            }}
          />
          <PasswordField
            label="Password"
            name="password"
            autoComplete="new-password"
            descriptiveText="Password must be at least 8 characters"
            value={password}
            onChange={(e) => {
              setPassword(e.target.value);
            }}
          />

          <Button
            type="submit"
            onClick={async (e) => {
              e.preventDefault();
              await registerUser({
                username,
                password,
              }).then((x) => {
                if (x == true) {
                  setSucceeded(true);
                }
              });
            }}
          >
            Sign Up
          </Button>
          {succeeded && (
            <Alert variation="success" heading="Account created">
              Log in via <Link href="/">Home Page</Link>
            </Alert>
          )}
        </Flex>
      </Card>
    </Grid>
  );
};

export default Signup;
